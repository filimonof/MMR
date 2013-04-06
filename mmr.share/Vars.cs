using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.share
{
    /// <summary>
    /// Переменные и константы проекта
    /// </summary>
    public static class Vars
    {

        //todo : либо установить значения при загрузке либо разпарсить App.config

        /// <summary>
        /// Строка подключения к основной базе
        /// </summary>
        public static string CON_STR
        {
            //get { return Properties.Settings.Default.mmr_dbConnectionString; }
            get { return @"Data Source=.\SQLEXPRESS;Initial Catalog=mmr.db;Integrated Security=True"; }
        }

        /// <summary>
        /// Строка подключения к базе прогноза
        /// </summary>
        public static string CON_STR_PROGNOZ
        {
            //todo : вывести в настройки 

            //get { return Properties.Settings.Default.prognoz_dbConnectionString; }
            get { return @"Data Source=BRSQL;Initial Catalog=PROGNOZ;User ID=reader;Pwd=reader"; }
        }

    }
}
