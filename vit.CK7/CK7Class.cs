/* 
 * Filimonov.Vitaliy@gmail.com 
 */

/* ������ �������������
    using vit.CK;
    CK7Class ck = new CK7Class();
    ck.SQLServers.Add(new SQLServerCK7("oik07-1-mrdv"));
    ck.SQLServers.Add(new SQLServerCK7("oik07-2-mrdv", "login", "pas"));           
    if (ck.OpenConnection())
    {
        TI ti = new TI(2509, CategoryTI.TI);
        OutData[] data = ck.GetValue(ti, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-5), 30);
        if (data != null)                    
        {
            foreach (OutData d in data)
            {                
                richTextBox1.Text += String.Format("\r\n \t {0} \t {1:F2} \t {2} \t", d.DTime.ToString("u"), d.Value, d.Sign);
            }
        }
        ck.CloseConnection();
    }
    else
        richTextBox1.Text += "��� ����������� � ��-2007\r\n";
 
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace vit.CK7
{
    #region class CK7Exception : Exception
    ///<summary>
    ///������ ������ Ck7GetData
    ///</summary>
    public class CK7Exception : Exception
    {
        public CK7Exception() : base() { }
        public CK7Exception(string message) : base(message) { }
        public CK7Exception(string message, Exception innerException) : base(message, innerException) { }
    }
    #endregion
    
    /// <summary>
    /// ��������� ������ �� �� 2007
    /// </summary>
    public class CK7Class
    {
        /// <summary>
        /// ��������� ������
        /// </summary>
        public string Caption { get; set; }

        /// <summary> 
        /// ���� ������� �������� �������� (� ��������)
        /// </summary>
        private ushort _timeout = 30;

        /// <summary>
        /// ����� �������� �������� (� ��������)
        /// </summary>
        public ushort Timeout
        {
            get { return this._timeout; }
            set { this._timeout = ((value >= 1 && value <= 600) ? (ushort)value : (ushort)30); }
            //������� �� 1� �� 10 ���, �� ��������� 30�
        }

        /// <summary>
        /// ��� �������� SQL �������
        /// </summary>
        public string MainCK2007Server { get; private set; }

        /// <summary>
        /// ���� �� ���������� � ��������
        /// </summary>        
        public bool Connected { get; private set; }

        /// <summary>
        /// SQL �������
        /// </summary>
        public List<SQLServerCK7> SQLServers { get; set; }

        /// <summary>
        /// ���� ��� �������� ����������
        /// </summary>
        private SqlConnection _connection;
        
        /// <summary>
        /// �����������
        /// </summary> 
        public CK7Class()
        {
            this.Caption = @"������ ��� �����";
            this.Timeout = 30;
            this.Connected = false;
            this.MainCK2007Server = string.Empty;
            this.SQLServers = new List<SQLServerCK7>(3);
            this._connection = new SqlConnection();
        }

        /// <summary
        /// >�����������
        /// </summary> 
        public CK7Class(string caption)
            : this()
        {
            this.Caption = caption;
        }


        /// <summary>
        /// ���������� ������� ������
        /// </summary>
        private void GetMainSQLServer()
        {
            this.MainCK2007Server = string.Empty;

            if (this.SQLServers.Count < 1)
                throw new CK7Exception(string.Format(@"�� ������� ������� ��-2007"));

            //���������� ������� ��7 ��� ����������� ��������            
            foreach (SQLServerCK7 srv in this.SQLServers)
            {
                SQLServerCK7 testSrv = new SQLServerCK7();
                testSrv.Timeout = this.Timeout;
                testSrv.Server = srv.Server;
                testSrv.WinNTauth = srv.WinNTauth;
                testSrv.Login = srv.Login;
                testSrv.Pas = srv.Pas;
                SqlConnection connection = new SqlConnection(testSrv.ToString());
                try
                {
                    //����������� � �������
                    connection.Open();

                    //���� ������� ������������ �������� ������ �� ����������� �������� �������
                    SqlCommand command = new SqlCommand(@"[dbo].fn_GetMainOIKServerName", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = this.Timeout;
                    SqlParameter param = command.Parameters.Add(@"Return", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.ReturnValue;
                    try
                    {
                        command.ExecuteNonQuery();
                        this.MainCK2007Server = param.Value.ToString();
                    }
                    catch
                    {
                        this.MainCK2007Server = string.Empty;
                    }
                }
                catch
                {
                    //��� ������ ����������� � ������� ������ ������ �� ����
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

                //���� ������� ������ ����� , ������� ������ ���������
                if (this.MainCK2007Server != string.Empty)
                    break;
            }
        }


        /// <summary>
        /// ���������� � ������� �������� CK-2007
        /// </summary>
        /// <returns>���������� �� ���������� ������� ������</returns>
        public bool OpenConnection()
        {
            this.GetMainSQLServer();
            if (this.MainCK2007Server != string.Empty && this.SQLServers.Count > 0)
            {
                SQLServerCK7 mainSrv = new SQLServerCK7(this.MainCK2007Server);
                foreach (SQLServerCK7 srv in this.SQLServers)
                    if (srv.Server.Trim().ToLower().Equals(this.MainCK2007Server.Trim().ToLower()))
                    {
                        mainSrv.WinNTauth = srv.WinNTauth;
                        mainSrv.Login = srv.Login;
                        mainSrv.Pas = srv.Pas;
                    }

                this.CloseConnection();

                mainSrv.Timeout = this.Timeout;
                this._connection.ConnectionString = mainSrv.ToString();
                try
                {
                    this._connection.Open();
                    this.Connected = true;
                }
                catch (Exception ex)
                {
                    this.Connected = false;
                    throw new CK7Exception(string.Format(@"������ ����������� � �������� ������� {0} \n ��������� ������� �� ��������� ����������� � ���� � ���������� \n {1}", this.MainCK2007Server, ex.Message));
                }
            }
            else
            {
                this.Connected = false;
            }

            return this.Connected;
        }


        /// <summary>
        /// ��������� ����������
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (this._connection.State == ConnectionState.Open)
                    this._connection.Close();
            }
            catch (Exception ex)
            {
                throw new CK7Exception(string.Format(@"������ ��� �������� ���������� � ������� �������� ��-2007 {0}: \n {1}", this.MainCK2007Server, ex.Message));
            }
            finally
            {
                this.Connected = false;
            }
        }


        /// <summary>
        /// ��������� ������ ������ �� ���������� ������ �������
        /// </summary>
        /// <param name="ti">�������������</param>
        /// <param name="date">���� � �����</param>
        /// <returns>��������</returns>
        public OutData? GetValue(TI ti, DateTime date)
        {
            return this.GetValue(ti, date, date, 0)[0];
        }


        /// <summary>
        /// ��������� ������ �� ��� �� ����
        /// </summary>
        /// <param name="ti">�������������</param>
        /// <param name="date">����</param>
        /// <param name="min">��� � �������</param>
        /// <returns>��������</returns>
        public OutData[] GetValue(TI ti, DateTime date, int min)
        {
            //�������� ��������� ������� �������� ����� ������
            return this.GetValue(ti, date.Date, date.Date.AddDays(1), min);
        }


        /// <summary>
        /// ��������� ������ �� ��� �� �������� �������
        /// </summary>
        /// <param name="ti">�������������</param>
        /// <param name="date1">���� � ����� ������ ���������</param>
        /// <param name="date2">���� � ����� ����� ���������</param>
        /// <param name="min">��� � �������</param>
        /// <returns>������ �� ����������</returns>
        public OutData[] GetValue(TI ti, DateTime date1, DateTime date2, int min)
        {
            /*
                ��������� ������ �� �������� ������� � ����������� ����� (��������� ����)
                exec @i= [OIK].[dbo].StepLt 'H','4581,1015', '20101116 0:00:00',0, '20101116 10:00:00',0,3600,0
                4,6 ��������� ����� �� ������ �������� �������             
            */
            SqlCommand command = new SqlCommand(@"[dbo].StepLt", this._connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = this.Timeout;
            command.Parameters.Add("@Cat", SqlDbType.NVarChar, 1).Value = (char)ti.Category;
            command.Parameters.Add("@Ids", SqlDbType.VarChar, 300).Value = ti.ID.ToString();
            command.Parameters.Add("@Start", SqlDbType.DateTime).Value = date1;
            command.Parameters.Add("@StartIsSummer", SqlDbType.Bit).Value = 0;
            command.Parameters.Add("@Stop", SqlDbType.DateTime).Value = date2;
            command.Parameters.Add("@StopIsSummer", SqlDbType.Bit).Value = 0;
            command.Parameters.Add("@Step", SqlDbType.Int).Value = min * 60;
            command.Parameters.Add("@ShowSystemTime", SqlDbType.Bit).Value = 0;
            ArrayList alValue = new ArrayList(24); //������ � 24 ��������, ���� ��� ��� ����������
            try
            {
                //CommandBehavior.CloseConnection
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OutData data = new OutData(
                            (DateTime)reader["timeLt"],
                            (double)reader["value"],
                            (int)reader["QC"]
                        );
                        alValue.Add(data);
                    }
                    reader.Close();
                    alValue.TrimToSize();
                    return (OutData[])alValue.ToArray(typeof(OutData));
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new CK7Exception(string.Format("������ ��� ��������� ������ �� CK-2007 \n {0} ", ex.Message));
            }
        }


        /// <summary>
        /// �������� ������ � ������������� ����������
        /// </summary>
        /// <param name="data">�������� ������ ��������</param>
        /// <returns>������ � ������������� ����������</returns>
        public static OutData[] Max(OutData[] data)
        {
            double max = double.MinValue;
            uint count = 0;
            foreach (OutData d in data)
            {
                if (!d.IsNoData)
                {
                    if ((double)d.Value > max)
                    {
                        max = (double)d.Value;
                        count = 1;
                    }
                    else if ((double)d.Value == max)
                        count++;
                }
            }

            if (count == 0) return null;

            OutData[] outdata = new OutData[count];
            count = 0;
            foreach (OutData d in data)
                if (count < outdata.Length && !d.IsNoData && (double)d.Value == max)
                {
                    outdata[count] = d;
                    count++;
                }
            return outdata;
        }


        /// <summary>
        /// �������� ������ � ������������ ����������
        /// </summary>
        /// <param name="data">�������� ������ ��������</param>
        /// <returns>������ � ������������ ����������</returns>
        public static OutData[] Min(OutData[] data)
        {
            double min = double.MaxValue;
            uint count = 0;
            foreach (OutData d in data)
            {
                if (!d.IsNoData)
                {
                    if ((double)d.Value < min)
                    {
                        min = (double)d.Value;
                        count = 1;
                    }
                    else if ((double)d.Value == min)
                        count++;
                }
            }

            if (count == 0) return null;

            OutData[] outdata = new OutData[count];
            count = 0;
            foreach (OutData d in data)
                if (count < outdata.Length && !d.IsNoData && (double)d.Value == min)
                {
                    outdata[count] = d;
                    count++;
                }
            return outdata;
        }

    }

}