using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using vit.CK7;

namespace mmr.share
{
    /// <summary>
    /// Класс для работы с параметрами
    /// </summary>    
    public class ParameterClass<ParameterEnum> : IDisposable
        where ParameterEnum : struct
    {
        /// <summary>
        /// Строка подключения к SQL серверу
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Словарь для хранения параметров
        /// </summary>
        private Dictionary<string, string> _values;

        /// <summary>
        /// запрос к базе за параметрами
        /// </summary>
        private readonly string SQL_SELECT_PARAM = "SELECT * FROM tblParameters";

        /// <summary>
        /// конструктор
        /// <param name="connectionString">строка подключения к SQL серверу</param>        
        /// </summary>
        public ParameterClass(string connectionString)
        {
            this._values = new Dictionary<string, string>();
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Получить данные из базы в словарь
        /// </summary>
        public void Open()
        {
            SqlConnection conection = new SqlConnection(this._connectionString);
            SqlCommand command = new SqlCommand(this.SQL_SELECT_PARAM, conection);
            conection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            this._values.Clear();
            try
            {
                while (reader.Read())
                    this._values.Add(reader["Name"].ToString(), reader["Value"].ToString());
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// Получить параметр
        /// </summary>
        /// <typeparam name="T">Тип параметра</typeparam>
        /// <param name="name">название (перечисление enum) параметра</param>
        /// <param name="def">значение по умолчанию</param>
        /// <returns>значение параметра</returns>
        public T Get<T>(ParameterEnum name, T def)
        //where T : int, bool, string
        {
            //убрать для универсальности
            //if (!typeof(T).IsEnum) return def;

            if (string.IsNullOrEmpty(name.ToString()))
                return def;

            if (!this._values.ContainsKey(name.ToString()))
                return def;

            //вместо int.TryParse(this._values[name.ToString()].ToString(), out val)
            System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                return (T)converter.ConvertFromString(this._values[name.ToString()]);
            }
            else
                return def;
        }

        /// <summary>
        /// Получить параметр
        /// </summary>
        /// <param name="name">название (перечисление enum) параметра</param>
        /// <param name="def">значение по умолчанию</param>
        /// <returns>значение параметра</returns>
        public Category Get(ParameterEnum name, Category def)
        {
            if (string.IsNullOrEmpty(name.ToString()))
                return def;

            if (!this._values.ContainsKey(name.ToString()))
                return def;

            try
            {
                return (Category)Enum.Parse(typeof(Category), this._values[name.ToString()], true);
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// Сохранить параметр
        /// </summary>
        /// <typeparam name="T">Тип параметра</typeparam>
        /// <param name="name">название (перечисление enum) параметра</param>
        /// <param name="val">новое значение</param>
        public void Set<T>(ParameterEnum name, T val)
        //where T : int, bool, string
        {
            SqlConnection conection = new SqlConnection(this._connectionString);
            SqlCommand command = new SqlCommand("Parameter_Set", conection);
            conection.Open();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = name;
            command.Parameters.Add("@value", SqlDbType.NVarChar).Value = val.ToString();
            command.ExecuteNonQuery();

            if (this._values.ContainsKey(name.ToString()))
                this._values[name.ToString()] = val.ToString();
            else
                this._values.Add(name.ToString(), val.ToString());
        }

        /// <summary>
        /// Сохранить параметр (категорию ТИ)
        /// </summary>
        /// <param name="name">название (перечисление enum) параметра</param>
        /// <param name="val">новое значение категории</param>
        public void Set(ParameterEnum name, Category val)
        {
            this.Set<string>(name, val.ToString());
        }

        /// <summary>
        /// Полчение параметра
        /// </summary>
        /// <typeparam name="T">Тип параметра</typeparam>
        /// <param name="connectionString">Строка подключения к SQL серверу</param>
        /// <param name="name">название (перечисление enum) параметра</param>
        /// <param name="def">значение по умолчанию</param>
        /// <returns>значение параметра</returns>
        public static T GetParam<T>(string connectionString, ParameterEnum name, T def)
        {
            ParameterClass<ParameterEnum> param = new ParameterClass<ParameterEnum>(connectionString);
            param.Open();
            return param.Get<T>(name, def);
        }

        #region Dispose() - уделание ресурсов класса

        /// <summary>
        /// поршло удаление ресурсов 
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// ручная очистка ресурсов
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// деструктор - завершитель запускаемый сборщиком мусора
        /// </summary>
        ~ParameterClass()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// освобождение занятых ресурсов
        /// </summary>
        /// <param name="disposing">true - ручная очистка; false - сборщиком мусора</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //освобождаем управляемые ресурсы -> component.Dispose();
                    this._values.Clear();
                    this._values = null;
                    //
                }
                //особождение неуправляемых ресурсов -> CloseHandle(handle);handle = IntPtr.Zero;
                this._disposed = true;
            }
        }

        ///// <summary>
        ///// пример класса наследника
        ///// </summary>
        //public class SupremeParameterClass : ParameterClass
        //{
        //    private bool _disposed = false;
        //    protected override void Dispose(bool disposing)
        //    {
        //        if (!this._disposed)
        //        {
        //            if (disposing)
        //            {
        //                // освобождаем управляемые ресурсы
        //            }
        //            // освобождаем неуправляемые ресурсы
        //            base.Dispose(disposing);
        //            this._disposed = true;
        //        }
        //    }
        //}
        #endregion

    }
}
