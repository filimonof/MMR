using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace mmr.share
{
    /// <summary>
    /// Класс для работы с прогнозом потребления
    /// </summary>
    public static class PrognozUtils
    {
        #region public GetPrognoz() - получить данные плановые
        /// <summary>Получение спрогназированных данных</summary>
        /// <param name="stringConnection">Подключение к базе данных</param>
        /// <param name="dateBegin">начиная с даты</param>
        /// <param name="dateEnd">заканчивая датой</param>
        /// <param name="shema">схема</param>
        /// <param name="areaID">Зона</param>
        /// <param name="status">Статус прогноза</param>          
        /// <returns>DataSet с данными</returns>        
        public static DataSet GetPrognoz(string stringConnection, DateTime dateBegin, DateTime dateEnd, int shema, int areaID, int status)
        {
            //exec GetPrognoz @DateBeg, @DateEnd, @Area_id, @status, @shema_id
            //    @status для РДУ - 3 (полный список - см. таблицу StatPrognoz)   
            //    @shema_id int=null
            SqlConnection myConn = new SqlConnection(stringConnection);
            SqlDataAdapter myData = new SqlDataAdapter("GetPrognoz", myConn);
            myData.SelectCommand.CommandType = CommandType.StoredProcedure;
            myData.SelectCommand.Parameters.Add(new SqlParameter("@DateBeg", SqlDbType.DateTime));
            myData.SelectCommand.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime));
            myData.SelectCommand.Parameters.Add(new SqlParameter("@Area_id", SqlDbType.Int));
            myData.SelectCommand.Parameters.Add(new SqlParameter("@status", SqlDbType.Int));
            myData.SelectCommand.Parameters.Add(new SqlParameter("@shema_id", SqlDbType.Int));
            myData.SelectCommand.Parameters["@DateBeg"].Value = dateBegin.ToUniversalTime();
            myData.SelectCommand.Parameters["@DateEnd"].Value = dateEnd.ToUniversalTime();
            myData.SelectCommand.Parameters["@Area_id"].Value = areaID;
            myData.SelectCommand.Parameters["@status"].Value = status;
            myData.SelectCommand.Parameters["@shema_id"].Value = shema;

            DataSet ds = new DataSet();
            myData.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
                if (row[0] != null)
                    row[0] = ((DateTime)row[0]).ToLocalTime();

            return ds;
        }

        /// <summary>Получение спрогназированных данных за день</summary>        
        /// <param name="stringConnection">Подключение к базе данных</param>
        /// <param name="date">Начиная с этой даты берём 24 часа</param>
        /// <param name="shema">схема</param>
        /// <param name="areaID">Зона</param>
        /// <param name="status">Статус прогноза</param>      
        /// <returns>DataSet с данными</returns>
        public static DataSet GetPrognoz(string stringConnection, DateTime date, int shema, int areaID, int status)
        {
            return GetPrognoz(stringConnection, date, date.AddDays(1), shema, areaID, status);
        }
        #endregion

        #region public  GetFacts() - получить данные фактические
        /// <summary>Получение фактических данных потребления</summary>
        /// <param name="stringConnection">Подключение к базе данных</param>
        /// <param name="dateBegin">начиная с даты</param>
        /// <param name="dateEnd">заканчивая датой</param>
        /// <param name="shema">схема</param>
        /// <param name="areaID">Зона</param>
        /// <param name="shag">шаг фактических данных (в минутах)</param>        
        /// <returns>DataSet с данными</returns>        
        public static DataSet GetFacts(string stringConnection, DateTime dateBegin, DateTime dateEnd, int shema, int areaID, int shag)
        {
            //exec GetAreaFacts @DateBeg, @DateEnd ,@Area_id, @shag, @shema_id 
            //    @status для РДУ - 3 (полный список - см. таблицу StatPrognoz)   
            //    @shag int=60,  -- в минутах
            //    @shema_id int=null
            SqlConnection myConn = new SqlConnection(stringConnection);
            SqlDataAdapter myData = new SqlDataAdapter("GetAreaFakts", myConn);
            myData.SelectCommand.CommandType = CommandType.StoredProcedure;
            myData.SelectCommand.Parameters.Add(new SqlParameter("@DateBeg", SqlDbType.DateTime));
            myData.SelectCommand.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime));
            myData.SelectCommand.Parameters.Add(new SqlParameter("@Area_id", SqlDbType.Int));
            myData.SelectCommand.Parameters.Add(new SqlParameter("@shag", SqlDbType.Int));
            myData.SelectCommand.Parameters.Add(new SqlParameter("@shema_id", SqlDbType.Int));
            myData.SelectCommand.Parameters["@DateBeg"].Value = dateBegin.ToUniversalTime();
            myData.SelectCommand.Parameters["@DateEnd"].Value = dateEnd.ToUniversalTime();
            myData.SelectCommand.Parameters["@Area_id"].Value = areaID;
            myData.SelectCommand.Parameters["@shag"].Value = shag;
            myData.SelectCommand.Parameters["@shema_id"].Value = shema;

            DataSet ds = new DataSet();
            myData.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
                if (row[0] != null)
                    row[0] = ((DateTime)row[0]).ToLocalTime();

            return ds;
        }

        /// <summary>Получение фактических данных потребления за день</summary>
        /// <param name="stringConnection">Подключение к базе данных</param>
        /// <param name="Date">начиная с этой даты берём 24 часа</param>        
        /// <param name="shema">схема</param>
        /// <param name="AreaID">Зона</param>
        /// <param name="shag">шаг фактических данных (в минутах)</param>              
        /// <returns>DataSet с данными</returns>        
        public static DataSet GetFacts(string stringConnection, DateTime date, int shema, int areaID, int shag)
        {
            return GetFacts(stringConnection, date, date.AddDays(1), shema, areaID, shag);
        }
        #endregion

        #region public StatPrognoz() - cтатусы прогноза
        /// <summary>Статусы прогноза</summary>
        /// <param name="stringConnection">строка подключения</param>
        /// <returns>список - номер/название статуса</returns>
        public static Dictionary<int, string> StatPrognoz(string stringConnection)
        {
            /* CLS_StatPrognoz STATUS SMALLNAME DESCRIPTION
             * 	-10	согл. прогноз ЦДУ	полученный из ЦДУ прогноз
                -3	прогноз РДУ	сырой прогноз  РДУ
                -2	прогноз ОДУ	сырой прогноз ОДУ
                -1	прогноз ЦДУ	сырой прогноз ЦДУ
                0	согласованный	согласованный
                1	ЦДУ	акцептованный в ЦДУ прогноз
                2	ОДУ	акцептованный в ОДУ прогноз
                3	РДУ	акцептованный в РДУ прогноз		
             * 
            */
            SqlConnection myConn = new SqlConnection(stringConnection);
            SqlCommand command = new SqlCommand("SELECT * FROM CLS_StatPrognoz", myConn);
            command.CommandType = CommandType.Text;
            myConn.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            Dictionary<int, string> values = new Dictionary<int, string>();
            values.Clear();
            try
            {
                while (reader.Read())
                    values.Add((int)reader["STATUS"], (string)reader["DESCRIPTION"]);
                return values;
            }
            finally
            {
                reader.Close();
            }
        }
        #endregion

        #region public Shemas() - Схемы
        /// <summary>Схемы</summary>
        /// <param name="stringConnection">строка подключения</param>
        /// <returns>список - номер/схема</returns>
        public static Dictionary<int, string> Shemas(string stringConnection)
        {
            /* 
             * SHEMAS  SHEMA_ID SHORT_NAME DESCRIPTION
            */
            SqlConnection myConn = new SqlConnection(stringConnection);
            SqlCommand command = new SqlCommand("SELECT * FROM SHEMAS", myConn);
            command.CommandType = CommandType.Text;
            myConn.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            Dictionary<int, string> values = new Dictionary<int, string>();
            values.Clear();
            try
            {
                while (reader.Read())
                    values.Add((int)reader["SHEMA_ID"], (string)reader["SHORT_NAME"]);
                return values;
            }
            finally
            {
                reader.Close();
            }
        }
        #endregion

        #region public Areas() - территории
        /// <summary>Территории</summary>
        /// <param name="stringConnection">строка подключения</param>
        /// <param name="shema">схема</param>
        /// <returns>список - номер/территория</returns>
        public static Dictionary<int, string> Areas(string stringConnection, int shema)
        {
            /* 
             * BR_SHEMA_AREAS  SHEMA_ID AREA_ID SH_NAME 
            */
            SqlConnection myConn = new SqlConnection(stringConnection);
            SqlCommand command = new SqlCommand("SELECT AREA_ID, SH_NAME FROM BR_SHEMA_AREAS WHERE SHEMA_ID = @shema", myConn);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@shema", SqlDbType.Int)).Value = shema;
            myConn.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            Dictionary<int, string> values = new Dictionary<int, string>();
            values.Clear();
            try
            {
                while (reader.Read())
                    values.Add((int)reader["AREA_ID"], (string)reader["SH_NAME"]);
                return values;
            }
            finally
            {
                reader.Close();
            }
        }
        #endregion

        #region public DataSet GetOtklHour() - получить отклонения (часовые)
        /// <summary>Получение прогнозируемых данных потребления, фактических и отклонения((п-ф)/п %)</summary>
        /// <param name="stringConnection">Подключение к базе данных</param>
        /// <param name="dateBegin">начиная с даты</param>
        /// <param name="dateEnd">заканчивая датой</param>
        /// <param name="shema">схема</param>
        /// <param name="areaID">Зона</param>
        /// <param name="status">Статус прогноза</param>                    
        /// <returns>DataSet с данными</returns>        
        public static DataSet GetOtklHour(string stringConnection, DateTime dateBegin, DateTime dateEnd, int shema, int areaID, int status)
        {
            DataSet ds_p = GetPrognoz(stringConnection, dateBegin, dateEnd, shema, areaID, status);
            DataSet ds_f = GetFacts(stringConnection, dateBegin, dateEnd, shema, areaID, 60);
            ds_p.Tables[0].Columns.Remove("status");
            ds_p.Tables[0].Columns.Remove("regim_id");
            ds_p.Tables[0].Columns.Remove("area_id");
            ds_p.Tables[0].Columns.Add("Potr", typeof(double));
            ds_p.Tables[0].Columns.Add("Otkl", typeof(double));

            //todo - немного не корректно обрабатывается перход времени
            foreach (DataRow row_p in ds_p.Tables[0].Rows)
            {
                foreach (DataRow row_f in ds_f.Tables[0].Rows)
                {
                    if ((DateTime)(row_f["DT"]) == (DateTime)(row_p["dtPrognoz"]))
                    {
                        row_p["Potr"] = row_f["Potr"];
                        row_p["Otkl"] = ((double)(row_p["vPrognoz"]) - (double)(row_p["Potr"])) / (double)(row_p["vPrognoz"]) * 100;
                    }
                }
            }
            return ds_p;
        }

        /// <summary>Получение прогнозируемых данных потребления, фактических и отклонения((п-ф)/п %) за день</summary>        
        /// <param name="stringConnection">Подключение к базе данных</param>
        /// <param name="date">начиная с этой даты берём 24 часа</param>
        /// <param name="shema">схема</param>
        /// <param name="areaID">Зона</param>
        /// <param name="status">Статус прогноза</param>       
        /// <returns>DataSet с данными</returns>        
        public static DataSet GetOtklHour(string stringConnection, DateTime date, int shema, int areaID, int status)
        {
            return GetOtklHour(stringConnection, date, date.AddDays(1), shema, areaID, status);
        }
        #endregion

    }
}
