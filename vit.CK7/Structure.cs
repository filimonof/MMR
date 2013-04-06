using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vit.CK7
{

    #region struct TI
    /// <summary>
    /// Структура тлеизмерения
    /// </summary>
    public struct TI
    {
        /// <summary>
        /// номер телеизмерения
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// Категория телеизмерения
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="id">номер телеизмерения</param>
        /// <param name="category">Категория телеизмерения</param>
        public TI(uint id, Category category)
            : this()
        {
            this.ID = id;
            this.Category = category;
        }
    }
    #endregion
    
    #region struct OutData
    ///<summary>
    ///Результат запроса
    ///</summary>
    public struct OutData
    {
        /// <summary>
        /// время
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// значение
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// признак значения
        /// </summary>
        public int Sign { get; set; }

        /// <summary>
        /// установлен флаг "нет данных" 0x8000
        /// </summary> 
        public bool IsNoData
        {
            get { return this.Sign.ToString("X") == "8000"; }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="time">время</param>
        /// <param name="value">значение</param>
        /// <param name="sign">признак значения</param>
        public OutData(DateTime time, double value, int sign)
            : this()
        {
            this.Time = time;
            this.Value = value;
            this.Sign = sign;
        }
    }
    #endregion

    #region struct SQLServerCK7
    /// <summary>
    /// Параметры SQL сервера комплекса СК-2007
    /// </summary>
    public struct SQLServerCK7
    {
        /// <summary> 
        /// сервер 
        /// </summary>
        public string Server { get; set; }

        /// <summary> 
        /// использовать Win NT аутентификацию
        /// </summary>
        public bool WinNTauth { get; set; }

        /// <summary> 
        /// логин 
        /// </summary>
        public string Login { get; set; }

        /// <summary> 
        /// пароль 
        /// </summary>
        public string Pas { get; set; }

        /// <summary> 
        /// время на соединение 
        /// </summary>
        public ushort Timeout { get; set; }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="server">сервер</param>
        public SQLServerCK7(string server)
            : this()
        {
            this.Server = server;
            this.WinNTauth = true;
            this.Timeout = 30;
        }

        /// <summary> 
        /// конструктор 
        /// </summary>
        /// <param name="server">сервер</param>
        /// <param name="login">логин</param>
        /// <param name="pas">пароль</param>
        public SQLServerCK7(string server, string login, string pas)
            : this(server)
        {
            this.Login = login;
            this.Pas = pas;
            this.WinNTauth = false;
        }

        /// <summary>
        /// строка подключения к SQL серверу
        /// </summary>
        /// <returns>строка подключения</returns>
        public override string ToString()
        {
            string strCon = string.Format(@"Data Source={0};Initial Catalog={1}; Connection Timeout={2};", this.Server, @"OIK", this.Timeout);
            if (this.WinNTauth)
                strCon += @"Integrated Security=True";
            else
                strCon += string.Format(@"User ID={0};Pwd={1}", this.Login, this.Pas);
            return strCon;
        }
    }
    #endregion

}
