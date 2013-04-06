using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mmr.Prognoz;
using mmr.share;
using vit.CK7;

namespace mmr.Prognoz
{
    /// <summary>Форма отображения погноза потребления и отклонений</summary>
    public partial class PrognozOtkl : Form
    {
        /// <summary>Инициализация</summary>
        public PrognozOtkl()
        {
            InitializeComponent();
        }

        /// <summary>Закрыть форму</summary>
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>процедура расчета отклоенний</summary>
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            //получаем параметры
            uint paramTI;
            Category paramCat;
            bool paramAvg;
            int paramShema;
            int paramArea;
            int paramStatus;
            string paramColumn1;
            string paramColumn2;
            string ckName;
            bool ckEnabled, ck1WinNT, ck2WinNT, ck3WinNT;
            string ck1Server, ck1Login, ck1Pas;
            string ck2Server, ck2Login, ck2Pas;
            string ck3Server, ck3Login, ck3Pas;
            using (ParameterClass<ParameterPrognozEnum> param = new ParameterClass<ParameterPrognozEnum>(Vars.CON_STR))
            {
                param.Open();
                paramTI = (uint)param.Get(ParameterPrognozEnum.PrognozOtkl_CK_TI, 0);
                paramCat = param.Get(ParameterPrognozEnum.PrognozOtkl_CK_Cat, Category.TI);
                paramAvg = param.Get(ParameterPrognozEnum.PrognozOtkl_Avg, false);

                paramShema = param.Get(ParameterPrognozEnum.PrognozOtkl_PP_Shema, 0);
                paramArea = param.Get(ParameterPrognozEnum.PrognozOtkl_PP_Area, 0);
                paramStatus = param.Get(ParameterPrognozEnum.PrognozOtkl_PP_Status, 0);

                paramColumn1 = param.Get(ParameterPrognozEnum.PrognozOtkl_ColumnName, string.Empty);
                paramColumn2 = param.Get(ParameterPrognozEnum.PrognozOtkl_ColumnOtklName, string.Empty);
                //if (paramShema == 0) throw new Exception("Не указана схема в параметрах");                
            }

            using (ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(Vars.CON_STR))
            {
                ckName = param.Get(ParameterShareEnum.CK_Name, "MMR");
                ckEnabled = param.Get(ParameterShareEnum.CK_Enabled, false);

                ck1Server = param.Get(ParameterShareEnum.CK_Server1_Name, string.Empty);
                ck1WinNT = param.Get(ParameterShareEnum.CK_Server1_WinNT, false);
                ck1Login = param.Get(ParameterShareEnum.CK_Server1_Login, string.Empty);
                ck1Pas = param.Get(ParameterShareEnum.CK_Server1_Pas, string.Empty);

                ck2Server = param.Get(ParameterShareEnum.CK_Server2_Name, string.Empty);
                ck2WinNT = param.Get(ParameterShareEnum.CK_Server2_WinNT, false);
                ck2Login = param.Get(ParameterShareEnum.CK_Server2_Login, string.Empty);
                ck2Pas = param.Get(ParameterShareEnum.CK_Server2_Pas, string.Empty);

                ck3Server = param.Get(ParameterShareEnum.CK_Server3_Name, string.Empty);
                ck3WinNT = param.Get(ParameterShareEnum.CK_Server3_WinNT, false);
                ck3Login = param.Get(ParameterShareEnum.CK_Server3_Login, string.Empty);
                ck3Pas = param.Get(ParameterShareEnum.CK_Server3_Pas, string.Empty);
            }

            //определение даты 
            DateTime dt1 = new DateTime(this.dateTimePicker1.Value.Year, this.dateTimePicker1.Value.Month, this.dateTimePicker1.Value.Day, 0, 0, 0);

            //получаем данные прогноза
            DataSet ds = PrognozUtils.GetOtklHour(Vars.CON_STR_PROGNOZ, dt1, dt1.AddDays(1).AddHours(1), paramShema, paramArea, paramStatus);
            ds.Tables[0].Columns.Add("PotrOIK", typeof(double));
            ds.Tables[0].Columns.Add("OtklOIK", typeof(double));

            //работа с ОИКом
            if (ckEnabled)
            {
                OutData[] valueOIK;
                CK7Class ck = new CK7Class(ckName);               
                if (ck1WinNT) ck.SQLServers.Add(new SQLServerCK7(ck1Server));
                else ck.SQLServers.Add(new SQLServerCK7(ck1Server, ck1Login, ck1Pas));
                if (ck2WinNT) ck.SQLServers.Add(new SQLServerCK7(ck2Server));
                else ck.SQLServers.Add(new SQLServerCK7(ck2Server, ck2Login, ck2Pas));
                if (ck3WinNT) ck.SQLServers.Add(new SQLServerCK7(ck3Server));
                else ck.SQLServers.Add(new SQLServerCK7(ck3Server, ck3Login, ck3Pas));

                //подключаемся
                if (ck.OpenConnection())
                {
                    //получаем данные ОИК
                    TI ti = new TI(paramTI, paramCat);
                    valueOIK = ck.GetValue(ti, dt1.AddMinutes(-30), dt1.AddDays(1).AddHours(1), 30);
                    ck.CloseConnection();

                    if (paramAvg)
                    {
                        //усредняем (получас + час) / 2
                        for (int i = 0; i < valueOIK.Length - 1; i++)
                        {
                            if (valueOIK[i].Time.Minute == 0)
                                foreach (OutData d2 in valueOIK)
                                {
                                    if (!valueOIK[i].IsNoData && !d2.IsNoData
                                        && valueOIK[i].Time == d2.Time.AddMinutes(+30))
                                    {
                                        valueOIK[i].Value = (((double)valueOIK[i].Value + (double)d2.Value) / 2);
                                    }
                                }
                        }
                    }

                    //выводим в таблицу с отклонениями
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        foreach (OutData d in valueOIK)
                        {
                            //todo: проверить переход времени
                            if ((DateTime)(d.Time) == (DateTime)(row["dtPrognoz"])
                                && !d.IsNoData)
                            {
                                row["PotrOIK"] = d.Value;
                                row["OtklOIK"] = ((double)(row["vPrognoz"]) - (double)(d.Value)) / (double)(row["vPrognoz"]) * 100;
                            }
                        }
                    }
                }
            }

            //биндим грид
            BindingSource bindingSource1 = new BindingSource();
            bindingSource1.DataSource = ds.Tables[0];

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.Columns[0].HeaderText = "Дата";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[1].HeaderText = "Прогноз";
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].HeaderText = "Мгновенное потребление";
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].HeaderText = "Отклонение от мгновенного";
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].HeaderText = paramColumn1;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].HeaderText = paramColumn2;
            dataGridView1.Columns[5].Width = 120;
            for (int i = 1; i < 6; i++)
                dataGridView1.Columns[i].DefaultCellStyle.Format = "N";


        }

        /// <summary>выделяем ячейки с отклонением более 5 красным</summary>
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 || e.ColumnIndex == 3)
            {
                double d;
                if (double.TryParse(e.Value.ToString(), out d))
                {
                    if (Math.Abs(d) >= 5)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
        }


    }
}
