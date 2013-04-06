using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace mmr.share.PPBR
{
    /// <summary>
    /// статусные значения лога ППБР
    /// Значения таблицы tblPPBR_Status
    /// </summary>
    public enum StatusLog
    {
        /// <summary>пустой статус</summary>
        Empty = 0,

        /// <summary>получен ППБР</summary>
        Load_OK = 10,

        /// <summary>ошибка при получении ППБР</summary>
        Load_Error = 11,

        /// <summary>отправлен в СК-2003</summary>
        UploadCK_OK = 20,

        /// <summary>ошибка при отправке в СК-2003</summary>
        UploadCK_Error = 21,

        /// <summary>Макет ППБ удачно выслан</summary>
        SendPPBR_OK = 30,

        /// <summary>Ошибка при отправке ППБР</summary>
        SendPPBR_Error = 31

    }

    /// <summary>
    /// Логи (события) ППБР
    /// </summary>
    public static class LogClass
    {        
        #region Add - Добавить событие ППБР

        /// <summary>
        /// Добавить пустое событие ППБР
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="date">дата макета ППБР</param>
        /// <returns>ID в логах собятий ППБР</returns>
        public static int Add(string connectionString, DateTime date)
        {
            return LogClass.Add(connectionString, date, StatusLog.Empty, null);
        }

        /// <summary>
        /// Добавить событие ППБР
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="date">дата макета ППБР</param>
        /// <param name="status">статус</param>
        /// <param name="comment">коментарий</param>
        /// <returns>ID в логах собятий ППБР</returns>
        public static int Add(string connectionString, DateTime date, StatusLog status, string comment)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("tblPPBR_Logs_Add", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.Add(new SqlParameter("@dt", SqlDbType.DateTime)).Value = date;
            command.Parameters.Add(new SqlParameter("@status", SqlDbType.Int)).Value = (int)status;
            command.Parameters.Add(new SqlParameter("@comment", SqlDbType.NVarChar)).Value = comment;
            command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, ParameterDirection.Output, 
                false, 0, 0, "ID", DataRowVersion.Default, null));
            command.ExecuteNonQuery();
            return (int)command.Parameters["@id"].Value;
        }
        #endregion

        #region Update - Изменить события ППБР
        /// <summary>
        /// Изменить события ППБР
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="id">ID макета ППБР</param>
        /// <param name="status">новый статус</param>
        /// <param name="comment">коментарий</param>
        public static void Update(string connectionString, int id, StatusLog status, string comment)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("tblPPBR_Logs_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
            command.Parameters.Add(new SqlParameter("@status", SqlDbType.Int)).Value = (int)status;
            command.Parameters.Add(new SqlParameter("@comment", SqlDbType.NVarChar)).Value = comment;
            command.ExecuteNonQuery();            
        }
        #endregion

        #region GetLogID - получения ID последнего загруженного макета ППБР на дату        
        /// <summary>
        /// получения ID последнего загруженного макета ППБР на дату
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="date">дата макета ППБР</param>
        /// <returns>ID макета или null если нету</returns>
        public static int? GetLogID(string connectionString, DateTime date)
        {
            const string SQL_SELECT =
                "SELECT TOP 1 [ID] FROM tblPPBR_Logs "
                + " WHERE DateMaket = @DateMaket AND StatusID = @StatusID "
                + " ORDER BY DateEvent DESC";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(SQL_SELECT, connection);
            connection.Open();
            command.Parameters.Add("@DateMaket", SqlDbType.DateTime).Value = date;
            command.Parameters.Add("@StatusID", SqlDbType.Int).Value = (int)StatusLog.Load_OK;
            return (int?)command.ExecuteScalar();
        }
        #endregion

        #region GetDate - получение даты загруженного макета ППБР по ID
        /// <summary>
        /// получение даты загруженного макета ППБР по ID
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL серверу</param>
        /// <param name="logID">ID макета ППБР</param>
        /// <returns>дата макета или null если нету</returns>
        public static DateTime? GetDate(string connectionString, int logID)
        {
            const string SQL_SELECT =
                "SELECT TOP 1 [DateMaket] FROM tblPPBR_Logs "
                + " WHERE ID = @ID AND StatusID = @StatusID";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(SQL_SELECT, connection);
            connection.Open();
            command.Parameters.Add("@ID", SqlDbType.Int).Value = logID;
            command.Parameters.Add("@StatusID", SqlDbType.Int).Value = (int)StatusLog.Load_OK;
            return (DateTime?)command.ExecuteScalar();
        }
        #endregion
    }
}
