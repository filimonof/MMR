using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace mmr.PlanPotr
{
    /// <summary>
    /// Класс для работы с макетом прогноза потребления с сайта market
    /// </summary>
    public static class PlanPotrMaketClass
    {
        /// <summary>
        /// Получение данных макета прогноза потребления
        /// </summary>
        /// <param name="connectionString">строка подключения к серверу</param>
        /// <param name="date">дата</param>
        /// <param name="locationKPO">код кпо</param>
        /// <param name="node">название ноды в макете Values, SumValues </param>
        /// <returns>список со значениями</returns>
        public static Dictionary<int, decimal> GetData(string connectionString, DateTime date, int locationKPO, string node)
        {
            const string SQL_SELECT =
               "SELECT [DemInt], [Value] FROM [tblPlanPotr_DataMaket] "
                + " WHERE [HeaderID] = (SELECT [ID] FROM [tblPlanPotr_HeaderMaket] WHERE [DateMaket] = @date AND [LocationKPO] = @locationKPO) "
                + " AND [Node] = @node"
                + " ORDER BY [DemInt] ASC";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(SQL_SELECT, connection);
            command.CommandType = CommandType.Text;
            connection.Open();
            command.Parameters.Add("@date", SqlDbType.DateTime).Value = date;
            command.Parameters.Add("@locationKPO", SqlDbType.Int).Value = locationKPO;
            command.Parameters.Add("@node", SqlDbType.NVarChar, 50).Value = node;
            SqlDataReader reader = command.ExecuteReader();
            Dictionary<int, decimal> values = new Dictionary<int, decimal>();
            values.Clear();
            try
            {
                while (reader.Read())
                    values.Add(
                        (int)reader["DemInt"],
                        decimal.Parse((reader["Value"].ToString()))
                    );
                return values;
            }
            finally
            {
                reader.Close();
            }
        }


        /// <summary>
        /// Разбор и сохранения макета прогноза потребления в формате xml
        /// </summary>
        /// <param name="connectionString">строка подключения к серверу</param>
        /// <param name="maket">строка с содержимым макета</param>
        public static void Parse(string connectionString, string maket)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("PlanPotr_ParseMaket", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.Add(new SqlParameter("@maket", SqlDbType.Xml)).Value = maket;
            command.ExecuteNonQuery();
        }
    }
}
