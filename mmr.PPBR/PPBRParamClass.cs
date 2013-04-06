using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace mmr
{    
    class PPBRParamClass:  IDisposable 
    {
        private DataSetPPBR.tblPPBR_ParametersDataTable ParamTable;
        private DataSetPPBRTableAdapters.tblPPBR_ParametersTableAdapter ParamAdapter;

        //DataSetPPBRTableAdapters.TableAdapterManager manager;
        //IDbTransaction transaction;
        //DataSet newDS;

        /// <summary>
        /// конструктор параметров ППБР
        /// </summary>
        public PPBRParamClass()
        {
            //transaction = connection.BeginTransaction("SampleTransaction");
            this.ParamAdapter = new DataSetPPBRTableAdapters.tblPPBR_ParametersTableAdapter();
        }

        public void Save()
        {
            ParamTable.AcceptChanges();
            //newDS.AcceptChanges();
            //transaction.Commit();
        }

        public void Cancel()
        {
            ParamTable.RejectChanges();
            //newDS.RejectChanges();
            //transaction.Rollback();
        }

        public DataRow[] GetWorkParam()
        {
            this.ParamTable = this.ParamAdapter.GetData();
            return this.ParamTable.Select("Enabled=true", "[Order]");
        }

        public DataRow[] GetArhiveParam()
        {
            this.ParamTable = this.ParamAdapter.GetData();
            return this.ParamTable.Select("Enabled=false", "[Order]");
        }

        #region add del rename labelTime
        public void AddParam(string newParam)
        {
            this.ParamAdapter.Insert(newParam);
        }

        public void DelParam(int id)
        {
            this.ParamAdapter.Delete(id);
        }

        public void RenameParam(int id, string name)
        {
            this.ParamAdapter.Update(name, id);
        }

        public void MakeLabelTime(int id)
        {
            this.ParamAdapter.MakeLabelTime(id);
        }
        #endregion

        #region up down
        public void Up(int id)
        {
            this.ParamAdapter.ParamUp(id);
        }

        public void Down(int id)
        {
            this.ParamAdapter.ParamDown(id);
        }
        #endregion

        #region Enabled Disabled
        public void Enabled(int id)
        {
            this.ParamAdapter.ParamEnabled(id);
        }

        public void Disabled(int id)
        {
            this.ParamAdapter.ParamDisabled(id);
        }
        #endregion
                
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
        ~PPBRParamClass()
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
                    this.ParamTable.Dispose();
                    this.ParamTable = null;

                    this.ParamAdapter.Dispose();
                    this.ParamAdapter = null;
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

    /*
    class PPBRParamClass
    {
        /// <summary>
        /// Строка подключения к SQL серверу 
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// запрос к базе за параметрами
        /// </summary>
        private readonly string SQL_SELECT_PPBR = 
            "SELECT ID, Name, [Order], Enabled, IsLabelTime "
            + " FROM tblPPBR_Parameters "
            + " ORDER BY [Order], Enabled ";


            SqlConnection conection;            
            SqlDataAdapter adapter;
            DataSet dataset;
            SqlTransaction trans;

        /// <summary>
        /// конструктор параметров ППБР
        /// </summary>
        public PPBRParamClass(string connectionString)
        {
            this._connectionString = connectionString;

            conection = new SqlConnection(this._connectionString);            
            conection.Open();            
            adapter = new SqlDataAdapter(this.SQL_SELECT_PPBR, conection);            
            dataset = new DataSet();            
            adapter.Fill(dataset);
            trans = conection.BeginTransaction("f1");
        }

        public void Save()
        {
        }

        public void Cancel()
        {
            dataset.Tables[0].RejectChanges();
            dataset.RejectChanges();
            trans.Rollback("f1");
            trans = conection.BeginTransaction("f1");
        }

        public DataRow[] GetWorkParam()
        {
            
            //this.ParamTable = this.ParamAdapter.GetData();
            //return this.ParamTable.Select("Enabled=true", "[Order]");            
             
            return dataset.Tables[0].Select("Enabled=true", "[Order]");
           
            //this._values.Clear();            
            //while (reader.Read())                
                //this._values.Add(reader["Name"].ToString(), reader["Value"].ToString());

        }       

        public DataRow[] GetArhiveParam()
        {            
            return dataset.Tables[0].Select("Enabled=false", "[Order]");
        }

        #region add del rename labelTime
        public void AddParam(string newParam)
        {
        }

        public void DelParam(int id)
        {
        }

        public void RenameParam(int id, string name)
        {
        }

        public void MakeLabelTime(int id)
        {
        }
        #endregion

        #region up down
        public void Up(int id)
        {
            dataset.Tables[0].Rows
            SqlCommand cmd = new SqlCommand("UPDATE tblPPBR_Parameters SET [Order] = [Order] - 1 WHERE     (ID = @ID)", conection);
            cmd.ExecuteNonQuery();  
        }

        public void Down(int id)
        {
        }
        #endregion

        #region Enabled Disabled
        public void Enabled(int id)
        {
        }

        public void Disabled(int id)
        {
        }
        #endregion
    }
    
    */


}
