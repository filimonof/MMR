using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace mmr.share
{
    /// <summary>
    /// письма бывшие в обработке
    /// </summary>
    public static class MailParsed
    {
        /// <summary>
        /// обрабатывалось ли письмо
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL</param>
        /// <param name="mailID">Идентификатор письма</param>
        /// <returns>обрабатывалось ли ранее это письмо</returns>
        public static bool IsParsed(string connectionString, string mailID)
        {
            SqlConnection conection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("MailParsed_IsExists", conection);
            command.CommandType = CommandType.StoredProcedure;
            conection.Open();
            command.Parameters.Add(new SqlParameter("@MailID", SqlDbType.NVarChar, 100)).Value = mailID;
            command.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int, 0,
                ParameterDirection.ReturnValue, false, 0, 0, "Return", DataRowVersion.Default, null));
            command.ExecuteNonQuery();            
            return (int)command.Parameters["@ReturnValue"].Value == 0 ? false : true;
        }

        /// <summary>
        /// добавление в список нового обработанного письма
        /// </summary>
        /// <param name="connectionString">строка подключения к SQL</param>
        /// <param name="mailID">Идентификатор письма</param>
        public static void Add(string connectionString, string mailID)
        {
            SqlConnection conection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("MailParsed_Add", conection);
            command.CommandType = CommandType.StoredProcedure;
            conection.Open();
            command.Parameters.Add(new SqlParameter("@MailID", SqlDbType.NVarChar, 100)).Value = mailID;
            command.ExecuteNonQuery();
        }
    }
}
