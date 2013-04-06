using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using vit.progress;
using mmr.share;

namespace mmr.share.PPBR
{

    /// <summary>
    /// Ошибки при создании макета СК
    /// </summary>
    public class CKMaketException : Exception
    {
        public CKMaketException() : base() { }
        public CKMaketException(string message) : base(message) { }
        public CKMaketException(string message, Exception innerException) : base(message, innerException) { }
    }


    #region CKMaketStruct - Структура параметров данных в макете СК-2003
    /// <summary>
    /// Структура параметров данных в макете СК-2003
    /// </summary>
    public struct CKMaketStruct
    {        
        /*  TABLE [dbo].[tblPPBR_CKParameters]
            (
	            [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 	
	            [Codename] NVARCHAR(50) NOT NULL UNIQUE,
	            [ParameterID] INT NOT NULL,
	            [OnlyHourse] BIT NOT NULL DEFAULT ((1)),		
	            [ControlSumma] BIT NOT NULL DEFAULT ((0))
            );  
        */
        /// <summary>Имя - код параметра</summary>
        public string Codename { get; set; }

        /// <summary>ID парметра из макета ППБР</summary>
        public int ParameterID { get; set; }

        /// <summary>часовые значения (false - получасовые)</summary>
        public bool OnlyHourse { get; set; }

        /// <summary>Ставить ли в конце данных проверочную сумму</summary>
        public bool ControlSumma { get; set; }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="codename">Имя - код параметра</param>
        /// <param name="paramID">ID парметра из макета ППБР</param>
        /// <param name="onlyHourse">часовые значения (false - получасовые)</param>
        /// <param name="controlSumma">Ставить ли в конце данных проверочную сумму</param>
        public CKMaketStruct(string codename, int paramID, bool onlyHourse, bool controlSumma)
            : this()
        {
            this.Codename = codename;
            this.ParameterID = paramID;
            this.OnlyHourse = onlyHourse;
            this.ControlSumma = controlSumma;
        }    
    }
    #endregion


    /// <summary>
    /// Создание макета СК-2003
    /// </summary>
    public class CKMaketClass :  ProgressBaseClass
    {
        /// <summary>Строка подключения к SQL серверу</summary>
        private string _connectionString;

        /// <summary>Дата макета СК</summary>
        public DateTime DateMaket { get; private set; }

        /// <summary>Номер (ID) макета в Логе ППБР</summary>
        public int LogID { get; private set; }

        #region Конструкторы

        /// <summary>
        /// Конструктор для создания макета СК на дату
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="dateMaket">дата макета</param>
        public CKMaketClass(string connectionString, DateTime dateMaket) 
            : this(connectionString, dateMaket, null) { }

        /// <summary>
        /// Конструктор для создания макета СК на дату
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="dateMaket">дата макета</param>
        /// <param name="progress">ссылка на рабочий прогресбар</param>
        public CKMaketClass(string connectionString, DateTime dateMaket, ProgressDlg progress)
            : base(progress)
        {
            this.LogID = 0;
            this.DateMaket = dateMaket;
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Конструктор для создания макета СК из ID макета ППБР
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="logID">ID макета ППБР по которому создаем макет СК</param>
        public CKMaketClass(string connectionString, int logID) 
            : this(connectionString, logID, null) { }

        /// <summary>
        /// Конструктор для создания макета СК из ID макета ППБР 
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="logID">ID макета ППБР по которому создаем макет СК</param>
        /// <param name="progress">ссылка на рабочий прогресбар</param>
        public CKMaketClass(string connectionString, int logID, ProgressDlg progress) 
            : base(progress)
        {
            this.LogID = logID;
            this._connectionString = connectionString;
        }
        #endregion

        #region CreateMaket - Создание макета CK-2003
        /// <summary>
        /// Создание макета CK-2003
        /// </summary>
        public void CreateMaket()
        {
            this.WriteComment("Создание макета СК ");

            if (this.LogID == 0)
            {
                // по дате определяем с какими данными logID работаем, берём самую позднюю загрузку
                this.WriteComment("Определяем номер данных");
                int? id = LogClass.GetLogID(this._connectionString, this.DateMaket);
                if (!id.HasValue) throw new CKMaketException(string.Format("Нет загруженных данных ППБР на дату {0}", this.DateMaket.ToShortDateString()));
                this.LogID = (int)id;
            }
            else
            {
                // определяем дату макета по этому логу
                this.WriteComment("Определяем дату данных");
                DateTime? date = LogClass.GetDate(this._connectionString, this.LogID);
                if (!date.HasValue) throw new CKMaketException(string.Format("Нет загруженных данных ППБР с №{0}", this.LogID));
                this.DateMaket = (DateTime)date;
            }

            this.WriteComment("Получение параметров");
            string directory;
            string filename;
            string header;
            string footer;
            string decimalPoint;
            string separator;
            EncodingMaket? encoding;
            using (ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(this._connectionString))
            {
                param.Open();
                directory = param.Get(ParameterShareEnum.PPBR_CK_DirectoryOutput, string.Empty);
                filename = param.Get(ParameterShareEnum.PPBR_CK_FileName_Template, string.Empty);
                header = param.Get(ParameterShareEnum.PPBR_CK_Header, string.Empty);
                footer = param.Get(ParameterShareEnum.PPBR_CK_EndData, string.Empty);
                separator = param.Get(ParameterShareEnum.PPBR_CK_Separator, string.Empty);
                string encode = param.Get(ParameterShareEnum.PPBR_CK_Encode, string.Empty);
                decimalPoint = param.Get(ParameterShareEnum.PPBR_CK_DecimalPoint, string.Empty);
                if (directory == string.Empty) throw new CKMaketException("Не указана директория назначения в параметрах макета СК-2003");
                if (!Directory.Exists(directory)) throw new CKMaketException(string.Format("Не существует директория назначения указаная в параметрах макета СК-2003: \n {0}", directory));
                if (directory == string.Empty) throw new CKMaketException("Не указан шаблон имени файла в параметрах макета СК-2003");
                if (header == string.Empty) throw new CKMaketException("Не указан заголовок файла в параметрах макета СК-2003");
                if (footer == string.Empty) throw new CKMaketException("Не указана концовка файла в параметрах макета СК-2003");
                if (separator == string.Empty) throw new CKMaketException("Не указан разделитель в параметрах макета СК-2003");
                if (encode == string.Empty) throw new CKMaketException("Не указана кодировка файла в параметрах макета СК-2003");
                encoding = EncodeUtils.GetEncodingMaket(encode);
                if (!encoding.HasValue) throw new CKMaketException("Не верно указана кодировка файла в параметрах макета СК-2003");
            }

            this.WriteComment("Получение параметров данных");
            List<CKMaketStruct> paramMaket = new List<CKMaketStruct>();       
            CKMaketClass.FillParamCKMaket(this._connectionString, ref paramMaket);
            if (paramMaket.Count == 0)
                throw new CKMaketException("Не определены данные для передачи в параметрах макета СК-2003");

            //результирующий файл
            string completFilename = Path.Combine(directory, ParseUtils.PasteDateInString(filename, this.DateMaket));
            //string completFilename =
            //    directory + (directory.TrimEnd().EndsWith(@"\") ? string.Empty : @"\")
            //    + PPBR_CKMaketClass.PasteDateInFilename(filename, this.DateMaket);

            //временый файл
            this.WriteComment("Создается временный файл");
            string tempFilename = Path.GetTempFileName();
            this.WriteComment(" " + tempFilename);

            FileStream fs = new FileStream(tempFilename, FileMode.Create, FileAccess.Write, FileShare.Read);
            using (StreamWriter sw = new StreamWriter(fs, EncodeUtils.GetEncoding((EncodingMaket)encoding)))
            {
                sw.Write(ParseUtils.PasteDateInString(header, this.DateMaket));

                for (int i = 0; i < paramMaket.Count; i++)
                {
                    this.WriteComment(string.Format("Параметр {0}", paramMaket[i].Codename));

                    List<decimal?> valueMaket = new List<decimal?>();
                    CKMaketClass.FillDataCKMaket(this._connectionString, this.LogID, paramMaket[i], ref valueMaket);

                    //если данных нету то ничено не выводим
                    if (valueMaket.Count == 0)
                    {
                        valueMaket.Clear();
                        valueMaket = null;
                        continue;
                    }

                    sw.WriteLine();
                    decimal suma = 0;
                    
                    sw.Write(paramMaket[i].Codename);
                    for (int j = 0; j < valueMaket.Count; j++)
                    {
                        if (valueMaket[j].HasValue) suma += (decimal)valueMaket[j];
                        sw.Write(separator);
                        if (valueMaket[j].HasValue) sw.Write(((decimal)valueMaket[j]).ToString().Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, decimalPoint).TrimEnd('0').TrimEnd(decimalPoint.ToCharArray()));
                    }
                    if (paramMaket[i].ControlSumma)
                        sw.Write(separator + suma.ToString().Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, decimalPoint).TrimEnd('0').TrimEnd(decimalPoint.ToCharArray()));
                    sw.Write(separator);

                    valueMaket.Clear();
                    valueMaket = null;
                }
                sw.WriteLine(footer);

            }//sw.Close;  формирование файла закончено          

            paramMaket.Clear();
            paramMaket = null;
            //conection.Close();

            this.WriteComment("Переносим макет в папку назначения");
            if (File.Exists(completFilename))
            {
                this.WriteComment(string.Format("Макет {0} существует, перезаписываем его", completFilename));
                File.Delete(completFilename);
            }
            File.Move(tempFilename, completFilename);

            this.WriteComment("Создание макета и выгрузка в СК закончена успешно");
        }
        #endregion

        #region FillParamCKMaket - получение параметров данных макета СК
        /// <summary>
        /// получение параметров данных макета СК
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="param">список с параметрами</param>
        public static void FillParamCKMaket(string connectionString, ref List<CKMaketStruct> param)
        {
            const string SQL_SELECT = "SELECT * FROM tblPPBR_CKParameters";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(SQL_SELECT, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            try
            {
                while (reader.Read())
                    param.Add(new CKMaketStruct((string)reader["Codename"], (int)reader["ParameterID"],
                            (bool)reader["OnlyHourse"], (bool)reader["ControlSumma"]));
            }
            finally
            {
                reader.Close();
            }
        }
        #endregion

        #region FillDataCKMaket - Получение данных параметра ППБР для макета СК
        /// <summary>
        /// Получение данных параметра ППБР для макета СК
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="logID">ID макета ППБР</param>
        /// <param name="param">парметр</param>
        /// <param name="value">массив значений</param>
        private static void FillDataCKMaket(string connectionString, int logID, CKMaketStruct param, ref List<decimal?> value)
        {
            string select =
                " SELECT d.[Value] "
                + " FROM (SELECT * FROM tblTime WHERE ID in ( {0} ) ) t "
                + " LEFT JOIN (SELECT * FROM tblPPBR_Data WHERE LogID = @LogID AND ParameterID = @ParameterID) d "
                + " ON d.TimeID = t.ID "
                + "ORDER BY t.ID ";
            //берем строку со списком timeID без перехода времени
            string strHourID = param.OnlyHourse ? TimeUtils.GetStringAllHourID(",", false) : TimeUtils.GetStringAllHalfeHourID(",", false);
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(string.Format(select, strHourID), connection);
            connection.Open();
            command.Parameters.Add("@LogID", SqlDbType.Int).Value = logID;
            command.Parameters.Add("@ParameterID", SqlDbType.Int).Value = param.ParameterID;            
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            try
            {
                while (reader.Read())
                {
                    decimal test;
                    if (decimal.TryParse(reader["Value"].ToString(), out test))
                        value.Add((decimal?)test);
                    else
                        value.Add(null);
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
