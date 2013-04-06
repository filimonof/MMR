using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using vit.progress;
using mmr.share;

namespace mmr.share.PPBR
{
    /// <summary>
    /// Ошибки при создании или парсинге макета ППБР
    /// </summary>
    public class MaketException : Exception
    {
        public MaketException() : base() { }
        public MaketException(string message) : base(message) { }
        public MaketException(string message, Exception innerException) : base(message, innerException) { }
    }

    #region MaketStruct - Структура данных макета ППБР
    /// <summary>
    /// Структура данных макета ППБР
    /// </summary>
    public struct MaketStruct
    {
        /*  CREATE TABLE [dbo].[tblPPBR_Parameters]
            (
	            [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	            [Name] NVARCHAR(50) NOT NULL UNIQUE,
	            [Order] INT NULL,
                [Enabled] BIT NOT NULL DEFAULT ((1)),
                [IsLabelTime] BIT NOT NULL DEFAULT ((0))
            );
        */
        /// <summary>ID</summary>
        public int ID { get; set; }

        /// <summary>Имя параметра</summary>
        public string Name { get; set; }

        /// <summary>Порядок в макета</summary>
        public int Order { get; set; }

        /// <summary>Используется</summary>
        public bool Enabled { get; set; }

        /// <summary>метка времени (только одна)</summary>
        public bool IsLabelTime { get; set; }

        /// <summary>
        /// Конструктор структуры
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Имя параметра</param>
        /// <param name="order">Порядок в макета</param>
        /// <param name="enabled">Используется</param>
        /// <param name="isLabelTime">метка времени (только одна)</param>
        public MaketStruct(int id, string name, int order, bool enabled, bool isLabelTime)
            : this()
        {
            this.ID = id;
            this.Name = name;
            this.Order = order;
            this.Enabled = enabled;
            this.IsLabelTime = isLabelTime;
        }
    }
    #endregion

    /// <summary>
    /// Класс создания и парсинга макета ППБР
    /// </summary>
    public class MaketClass : ProgressBaseClass
    {
        /// <summary>Строка подключения к SQL серверу</summary>
        private string _connectionString;

        /// <summary>Дата макета ППБР</summary>
        public DateTime DateMaket { get; private set; }

        /// <summary>Номер (ID) макета в Логе ППБР</summary>
        public int LogID { get; private set; }

        /// <summary>Имя файла макета</summary>
        public string FileName { get; set; }

        #region Конструкторы
        public MaketClass(string connectionString, DateTime dateMaket)
            : this(connectionString, dateMaket, null) { }

        public MaketClass(string connectionString, DateTime dateMaket, ProgressDlg progress)
            : base(progress)
        {
            this._connectionString = connectionString;
            this.DateMaket = dateMaket;
            this.LogID = 0;
        }

        public MaketClass(string connectionString, int logID)
            : this(connectionString, logID, null) { }

        public MaketClass(string connectionString, int logID, ProgressDlg progress)
            : base(progress)
        {
            this._connectionString = connectionString;
            this.LogID = logID;
        }

        public MaketClass(string connectionString, string filename, int logID)
            : this(connectionString, filename, logID, null) { }

        public MaketClass(string connectionString, string filename, int logID, ProgressDlg progress)
            : this(connectionString, logID, progress)
        {
            this.FileName = filename;
        }
        #endregion

        #region CreateMaket - Создание макета
        /// <summary>
        /// создание макета ППБР
        /// </summary>           
        public void CreateMaket()
        {
            this.WriteComment("Начало создания макета ");
            this.FileName = string.Empty;

            if (this.LogID == 0)
            {
                // по дате определяем с какими данными logID работаем, берём самую позднюю загрузку
                this.WriteComment("Определяем номер данных");
                int? id = LogClass.GetLogID(this._connectionString, this.DateMaket);
                if (!id.HasValue) throw new MaketException(string.Format("Нет загруженных данных ППБР на дату {0}", this.DateMaket.ToShortDateString()));
                this.LogID = (int)id;
            }
            else
            {
                // определяем дату макета по этому логу
                this.WriteComment("Определяем дату данных");
                DateTime? date = LogClass.GetDate(this._connectionString, this.LogID);
                if (!date.HasValue) throw new MaketException(string.Format("Нет загруженных данных ППБР с №{0}", this.LogID));
                this.DateMaket = (DateTime)date;
            }

            this.WriteComment("Получение параметров ППБР");
            string separator;
            string header;
            int beginData;
            string templateFileName;
            string decimalPoint;
            EncodingMaket? encoding;
            using (ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(this._connectionString))
            {
                param.Open();
                separator = param.Get(ParameterShareEnum.PPBR_Separator, string.Empty);
                header = param.Get(ParameterShareEnum.PPBR_Header, string.Empty);
                beginData = param.Get(ParameterShareEnum.PPBR_BeginData, 0);
                templateFileName = param.Get(ParameterShareEnum.PPBR_FileName_Template, string.Empty);
                string encode = param.Get(ParameterShareEnum.PPBR_Encode, string.Empty);
                decimalPoint = param.Get(ParameterShareEnum.PPBR_DecimalPoint, string.Empty);
                if (separator == string.Empty) throw new MaketException("Не указан разделитель в параметрах макета ППБР");
                if (header == string.Empty) throw new MaketException("Не указан заголовок файла в параметрах макета ППБР");
                if (beginData == 0) throw new MaketException("В параметрах макета ППБР не корректная строка начала данных");
                if (beginData == 1) throw new MaketException("Строка начала данных в параметрах ППБР не может быть первой (первая строка для заголовка макета)");
                if (encode == string.Empty) throw new MaketException("Не указана кодировка файла в параметрах макета ППБР");
                encoding = EncodeUtils.GetEncodingMaket(encode);
                if (!encoding.HasValue) throw new MaketException("Не верно указана кодировка файла в параметрах макета ППБР");
            }

            this.WriteComment("Получение рабочих параметров ППБР");
            List<MaketStruct> paramPPBR = new List<MaketStruct>();
            MaketClass.FillParamMaket(this._connectionString, ref paramPPBR);
            if (paramPPBR.Count == 0)
                throw new MaketException("Не настроены рабочие параметры макета ППБР");
            int hour_index = -1; //номер временного параметра 
            string twoString = string.Empty;
            for (int i = 0; i < paramPPBR.Count; i++)
            {
                if (paramPPBR[i].IsLabelTime) hour_index = i;
                twoString += paramPPBR[i].Name + separator;
            }
            if (hour_index == -1) throw new MaketException("Не настроена метка времени в рабочих параметрах макета ППБР");

            this.WriteComment("Создается временный файл");
            string tempFilename = Path.Combine(Path.GetTempPath()
                , ParseUtils.PasteDateInString(templateFileName, this.DateMaket));
            this.WriteComment(" " + tempFilename);
            if (File.Exists(tempFilename)) File.Delete(tempFilename);

            FileStream fs = new FileStream(tempFilename, FileMode.Create, FileAccess.Write, FileShare.Read);
            using (StreamWriter sw = new StreamWriter(fs, EncodeUtils.GetEncoding((EncodingMaket)encoding)))
            {
                sw.WriteLine(ParseUtils.PasteDateInString(header, this.DateMaket));
                sw.WriteLine(twoString);

                List<object[]> dat = new List<object[]>();
                MaketClass.FillDataMaket(this._connectionString, this.LogID, ref dat);

                decimal[] summa = new decimal[paramPPBR.Count];
                for (int i = 0; i < summa.Length; i++) summa[i] = 0;
                for (int i = 0; i < dat.Count; i++)
                {
                    object[] v = dat[i];
                    for (int j = 0; j < v.Length; j++)
                    {
                        if (v[j] != null)
                        {
                            if (j == hour_index)
                            {
                                string s = v[j].ToString().Substring(0, 2);
                                sw.Write(s.StartsWith("0") ? s.Substring(1, 1) : s);
                            }
                            else
                                sw.Write(v[j].ToString().Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, decimalPoint).TrimEnd('0').TrimEnd(decimalPoint.ToCharArray()));

                            decimal test;                            
                            if (decimal.TryParse(v[j].ToString(), out test))
                                summa[j] += test;
                        }
                        sw.Write(separator);
                    }
                    sw.WriteLine();
                }

                sw.Write("Сумма" + separator);
                for (int i = 0; i < summa.Length; i++)
                    if (i != hour_index)
                        sw.Write(summa[i].ToString().Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, decimalPoint).TrimEnd('0').TrimEnd(decimalPoint.ToCharArray()) + separator);
            }

            this.WriteComment("Макет ППБР сформирован");
            this.FileName = tempFilename;
        }
        #endregion

        #region Парсинг данных - version 1
        public void Parse()
        {
            //todo : продумать транзакцию

            this.WriteComment("Начало обработки");

            StreamReader text = null;
            if (File.Exists(this.FileName))
            {
                string encode = ParameterClass<ParameterShareEnum>.GetParam(this._connectionString, ParameterShareEnum.PPBR_Encode, string.Empty);
                if (encode == string.Empty) throw new MaketException("Не указана кодировка файла в параметрах макета ППБР");
                EncodingMaket? encoding = EncodeUtils.GetEncodingMaket(encode);
                if (!encoding.HasValue) throw new MaketException("Не верно указана кодировка файла в параметрах макета ППБР");
                text = new StreamReader(this.FileName, EncodeUtils.GetEncoding((EncodingMaket)encoding));
            }
            else
            {
                throw new MaketException(string.Format("Файл {0} не найден", this.FileName));
            }
            if (text.Peek() <= 0) throw new MaketException("Макет пустой");

            this.WriteComment("Получение параметров ППБР");
            string separator;
            string decimalPoint;
            string header;
            int beginData;
            string templateFileName;
            using (ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(this._connectionString))
            {
                param.Open();
                separator = param.Get(ParameterShareEnum.PPBR_Separator, string.Empty);
                header = param.Get(ParameterShareEnum.PPBR_Header, string.Empty);
                beginData = param.Get(ParameterShareEnum.PPBR_BeginData, 0);
                templateFileName = param.Get(ParameterShareEnum.PPBR_FileName_Template, string.Empty);
                string encode = param.Get(ParameterShareEnum.PPBR_Encode, string.Empty);
                decimalPoint = param.Get(ParameterShareEnum.PPBR_DecimalPoint, string.Empty);
                if (separator == string.Empty) throw new MaketException("Не указан разделитель в параметрах макета ППБР");
                if (header == string.Empty) throw new MaketException("Не указан заголовок файла в параметрах макета ППБР");
                if (beginData == 0) throw new MaketException("В параметрах макета ППБР не корректная строка начала данных");
                if (beginData == 1) throw new MaketException("Строка начала данных в параметрах ППБР не может быть первой (первая строка для заголовка макета)");
            }

            //if (this._cancel) { this.ParseRollback(); return; }

            //проверка заголовка header и сравниение даты в имени файла и в header             
            string firstLine = text.ReadLine();
            if (!string.IsNullOrEmpty(this.FileName) && (templateFileName != string.Empty))
            {
                this.WriteComment("Проверка соответствия даты в имени файла и макете");
                DateTime dateMaket;
                ParseUtils.ParseDateInString(firstLine, header, out dateMaket);
                int dayFilename;
                int monthFilename;
                int yearFilename;
                ParseUtils.ParseDateInString(this.FileName, templateFileName, out dayFilename, out monthFilename, out yearFilename);
                if ((dayFilename != 0) && (dateMaket.Day != dayFilename))
                    throw new MaketException("Несоответствие номера дня в имени файла и в содержании макета");
                if ((monthFilename != 0) && (dateMaket.Month != monthFilename))
                    throw new MaketException("Несоответствие номера месяца в имени файла и в содержании макета");
                //todo : а нужно ли сравнивать год
                if ((yearFilename != 0) && (dateMaket.Year != yearFilename))
                    throw new MaketException("Несоответствие номера года в имени файла и в содержании макета");
            }

            //if (this._cancel) { this.ParseRollback(); return; }

            //рабочие параметры
            this.WriteComment("Получение рабочих параметров ППБР");
            List<MaketStruct> paramPPBR = new List<MaketStruct>();
            MaketClass.FillParamMaket(this._connectionString, ref paramPPBR);
            if (paramPPBR.Count == 0)
                throw new MaketException("Не настроены рабочие параметры макета ППБР");
            int hour_index = -1; //номер временного параметра 
            for (int i = 0; i < paramPPBR.Count; i++)
                if (paramPPBR[i].IsLabelTime)
                    hour_index = i;
            if (hour_index == -1) throw new MaketException("Не настроена метка времени в рабочих параметрах макета ППБР");

            //Перейти на строку с номером beginData
            for (int i = 1; i < beginData - 1; i++)
            {
                if (text.Peek() >= 0) text.ReadLine();
                else throw new MaketException("Неожиданный конец макета ППБР");
            }

            //if (this._cancel) { this.ParseRollback(); return; }

            string[] separatorArray = new string[1]; separatorArray[0] = separator;
            decimal[] summa = new decimal[paramPPBR.Count];
            for (int i = 0; i < paramPPBR.Count; i++) summa[i] = 0;
            if (text.Peek() < 0)
                throw new MaketException("Неожиданный конец макета ППБР");
            //последовательно парсить каждую строку
            while (text.Peek() >= 0)
            {
                System.Windows.Forms.Application.DoEvents();
                //if (this._cancel) { this.ParseRollback(); return; }

                //читаем строку из файла
                string line = text.ReadLine().Trim();
                if (line.Length > 0)
                {
                    //убираем последний разделитьель если есть
                    if (line.EndsWith(separator))
                        line = line.Remove(line.Length - separator.Length);
                    //разбиваем строку на значения
                    string[] value = line.Split(separatorArray, StringSplitOptions.None);
                    //если строка не разбилась
                    if (value.Length == 0)
                        throw new MaketException(string.Format("Строка \"{0}\" не  разбивается на значения разделителем \"{1}\"", line, separator));
                    //проверка является ли строка суммой
                    float test;
                    if (!float.TryParse(value[0], out test)) //первое значение не число? - в макете написано СУММА
                    {
                        this.WriteComment("Проверка суммы");
                        //при hour_index > 0 в масиве summa элемент времени с индексом hour_index перетащить в нулевую ячейку                        
                        for (int i = hour_index; i > 0; i--)
                        {
                            decimal tmp = summa[i];
                            summa[i] = summa[i - 1];
                            summa[i - 1] = tmp;
                        }
                        //проверка всех значений времени кроме 0 ячейки (там СУММА и "")
                        for (int i = 1; i < value.Length; i++)
                        {
                            decimal test_summa = 0;
                            //value[i] = value[i].Replace(decimalPoint, System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                            if (!decimal.TryParse(value[i].Replace(decimalPoint, System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator), out test_summa) || (test_summa != summa[i]))
                                throw new MaketException(string.Format("Ошибка в сумме значений макета: параметр {0} в макете {1} при подсчете {2}", i + 1, value[i], summa[i]));
                        }
                    }
                    else // если не сумма , значит строка с данными
                    {
                        if (value.Length != paramPPBR.Count)
                            throw new MaketException(string.Format("Параметры макета настроены не правильно: количество значений в макете и в настройках не совпадает ({0})", line));

                        // todo : продумать переход времени, т.е. как он обозначается значением

                        //выцепаем значение часа
                        int hour;
                        if (!int.TryParse(value[hour_index], out hour))
                            throw new MaketException(string.Format("Ошибка во временном параметре макета ППБР: строка \"{0}\" время \"{1}\"", line, value[hour_index]));

                        //суммируем для дальнейшей проверки суммы 
                        //и подготавливаем данные для хранимой процедуры
                        string sValue = "";
                        string sParam = "";
                        for (int i = 0; i < paramPPBR.Count; i++)
                            if (i != hour_index)
                            {                                
                                decimal val;
                                if (decimal.TryParse(value[i].Replace(decimalPoint, System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator), out val))
                                    summa[i] += (decimal)val;
                                //в SQL десятичная точка всегда .
                                sValue += value[i].Replace(decimalPoint, ".") + " ";
                                sParam += paramPPBR[i].ID.ToString() + " ";
                            }
                        //удаляем последний знак
                        sValue = sValue.Remove(sValue.Length - 1);
                        sParam = sParam.Remove(sParam.Length - 1);

                        // определить ID в таблице Time  по hour
                        int timeID = TimeUtils.GetTimeIDToHour(hour);
                        if (timeID == 0) throw new MaketException(string.Format("Некорректная метка времени \"{0}\" в строке \"{1}\"", hour.ToString(), line));
                        // выполнить хранимку на запись данных в базу
                        this.WriteComment(string.Format("Сохранение даных за {0} час", hour));
                        
                        MaketClass.AddData(this._connectionString, this.LogID, timeID, sParam, sValue);
                    }
                }
                //System.Threading.Thread.Sleep(1000);
                //this.Next(1);
            }
            this.WriteComment("Изменение статуса загрузки в логе ");
            LogClass.Update(this._connectionString, this.LogID, StatusLog.Load_OK, null);
            this.WriteComment("Обработка успешно закончена");
        }
        #endregion

        #region ParseRollback - Отмена парсинга
        /// <summary>
        /// Отмена парсинга
        /// </summary>
        /// <param name="error_text">сообщение об ошибке в лог</param>
        public void ParseRollback(string error_text)
        {
            this.WriteComment("Удаление ранее загруженных данных");
            MaketClass.DelData(this._connectionString, this.LogID);
            this.WriteComment("Изменение статуса загрузки в логе");
            LogClass.Update(this._connectionString, this.LogID, StatusLog.Load_Error, error_text);
        }
        #endregion

        #region AddData - добавление данных в базу
        /// <summary>
        /// добавление данных в базу
        /// </summary>
        /// <param name="connectionString">строка подключения к базе</param>
        /// <param name="logID">ID в логе</param>
        /// <param name="timeID">ID времени</param>
        /// <param name="param">параметр</param>
        /// <param name="value">значение</param>
        protected static void AddData(string connectionString, int logID, int timeID, string param, string value)
        {
            if ((param.Length >= 4000) || (value.Length >= 4000))
                throw new MaketException("Программа не рассчитана на такое большое число параметров: \n нельзя передать в SQL Server строку больше 4000 символов");

            SqlConnection conection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("tblPPBR_Data_Add", conection);
            command.CommandType = CommandType.StoredProcedure;
            conection.Open();
            command.Parameters.Add(new SqlParameter("@logID", SqlDbType.Int)).Value = logID;
            command.Parameters.Add(new SqlParameter("@timeID", SqlDbType.Int)).Value = timeID;
            command.Parameters.Add(new SqlParameter("@param", SqlDbType.NVarChar)).Value = param;
            command.Parameters.Add(new SqlParameter("@value", SqlDbType.NVarChar)).Value = value;
            command.ExecuteNonQuery();
        }
        #endregion

        #region DelData - удаление данных из базы
        /// <summary>
        /// удаление данных из базы
        /// </summary>
        /// <param name="connectionString">строка подключения к базе</param>
        /// <param name="logID">ID в логе</param>
        private static void DelData(string connectionString, int logID)
        {
            const string SQL_DEL = "DELETE FROM tblPPBR_Data WHERE LogID = @logID";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(SQL_DEL, connection);
            command.CommandType = CommandType.Text;
            connection.Open();
            command.Parameters.Add(new SqlParameter("@logID", SqlDbType.Int)).Value = logID;
            command.ExecuteNonQuery();
        }
        #endregion

        #region ParseDateInMaket  -  Извлечение даты из файла или из стрима
        /// <summary>
        /// Извлечение даты из стрима
        /// </summary>
        /// <param name="connectionString">строка подключения к базе</param>
        /// <param name="sr">стрим с содержимым файла</param>
        /// <returns></returns>
        public static DateTime ParseDateInMaket(string connectionString, StreamReader sr)
        {
            //получаеи заголовок из настроек файла
            string header = ParameterClass<ParameterShareEnum>.GetParam(connectionString, ParameterShareEnum.PPBR_Header, string.Empty);

            //читаем первую заголовочную строчку
            string firstLine = sr.ReadLine();

            DateTime dt;
            ParseUtils.ParseDateInString(firstLine, header, out dt);

            return dt;
        }

        /// <summary>
        /// Извлечение даты из файла
        /// </summary>
        /// <param name="connectionString">строка подключения к базе</param>
        /// <param name="fileName">имя файла</param>
        /// <returns></returns>
        public static DateTime ParseDateInMaket(string connectionString, string fileName)
        {
            if (File.Exists(fileName))
            {
                string encode = ParameterClass<ParameterShareEnum>.GetParam<string>(connectionString, ParameterShareEnum.PPBR_Encode, string.Empty);
                if (encode == string.Empty) throw new MaketException("Не указана кодировка файла в параметрах макета ППБР");
                EncodingMaket? encoding = EncodeUtils.GetEncodingMaket(encode);
                if (!encoding.HasValue) throw new MaketException("Не верно указана кодировка файла в параметрах макета ППБР");

                using (StreamReader sr = new StreamReader(fileName, EncodeUtils.GetEncoding((EncodingMaket)encoding)))
                {
                    return ParseDateInMaket(connectionString, sr);
                }
            }
            else throw new MaketException(string.Format("Файл \"{0}\" не найден", fileName));
        }
        #endregion

        #region FillParamMaket - получение параметров данных макета ППБР
        /// <summary>
        /// получение параметров данных макета ППБР
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="param">список с параметрами</param>
        private static void FillParamMaket(string connectionString, ref List<MaketStruct> param)
        {
            const string SQL_SELECT = "SELECT * FROM tblPPBR_Parameters WHERE Enabled=1 ORDER BY [Order]";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(SQL_SELECT, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            try
            {
                while (reader.Read())
                    param.Add(new MaketStruct((int)reader["ID"], (string)reader["Name"], (int)reader["Order"],
                            (bool)reader["Enabled"], (bool)reader["IsLabelTime"]));
            }
            finally
            {
                reader.Close();
            }
        }
        #endregion

        #region FillDataMaket - получение данных макета ППБР по номеру лога
        /// <summary>
        /// получение данных макета ППБР по номеру лога
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL Server</param>
        /// <param name="logID">ID макета ППБР в логах</param>
        /// <param name="value">значения</param>
        private static void FillDataMaket(string connectionString, int logID, ref List<object[]> value)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("tblPPBR_Data_CreateMaketPPBR", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.Add("@LogID", SqlDbType.Int).Value = logID;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            try
            {
                while (reader.Read())
                {
                    object[] val = new object[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                        val[i] = reader[i];
                    value.Add(val);
                }
            }
            finally
            {
                reader.Close();
            }
        }
        #endregion

        #region Dispose
        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // освобождаем управляемые ресурсы
                }
                // освобождаем неуправляемые ресурсы
                base.Dispose(disposing);
                this._disposed = true;
            }
        }
        #endregion

    }
}
