using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using mmr.share;
using mmr.plugins;
using vit.CK7;

namespace mmr.PlanPotr
{

    public partial class PlanPotrForm : SimpleMDIForm
    {
        /// <summary>главная таблица с расчетом</summary>
        private DataTable _tableMain;

        /// <summary>таблица с фактом потребления за предыдущие недели</summary>
        private DataTable _tableFact;

        /// <summary>Дата планирования прогноза</summary>
        private DateTime _dtPrognoz;

        /// <summary>Дата предыдущего дня</summary>
        private DateTime _dtPredDay;

        /// <summary>Дата начала недели фактического потребления</summary>
        private DateTime _dtStartFact;


        /// <summary>конструктор</summary>
        public PlanPotrForm()
        {
            InitializeComponent();
        }

        /// <summary>Закрыть форму</summary>
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>Процедура расчета и заполнения таблиц данными</summary>
        private void CalcAndFillTables()
        {
            //получаем параметры
            uint paramFackPotrTI, paramFackTempTI;
            Category paramFackPotrCat, paramFackTempCat;
            string ckName;
            bool ckEnabled, ck1WinNT, ck2WinNT, ck3WinNT;
            string ck1Server, ck1Login, ck1Pas;
            string ck2Server, ck2Login, ck2Pas;
            string ck3Server, ck3Login, ck3Pas;
            int locationKPO;
            using (ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(Vars.CON_STR))
            {
                param.Open();

                //todo : и температура и потребление в настройки

                paramFackPotrTI = 4484; // param.Get(ParameterName.PlanPotr_FactPotr_TI, 0);
                paramFackPotrCat = Category.PV; //param.Get(ParameterName.PlanPotr_FactPotr_Cat, CategoryTI.TI);                

                paramFackTempTI = 2509;  //param.Get(ParameterName.PlanPotr_FactTemperatura_TI, 0);
                paramFackTempCat = Category.TI; //param.Get(ParameterName.PlanPotr_FactTemperatura_Cat, CategoryTI.TI);                

                locationKPO = 316770; //param.Get(ParameterName.PlanPotr_FactTemperatura_TI, 0);

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

            //создаём главную таблицу                        
            this._tableMain = new DataTable("TableMain");
            this._tableMain.Columns.Add(new DataColumn("Hour", typeof(int))); //Час
            this._tableMain.Columns.Add(new DataColumn("ConsumptionMarketPlayers", typeof(decimal)));      //Потребление субъектов рынка
            this._tableMain.Columns.Add(new DataColumn("ActualConsumptionPreviousDay", typeof(decimal)));  //Фактическое потребление предыдущий суток
            this._tableMain.Columns.Add(new DataColumn("CloudinessPreviousDay", typeof(int)));            //Облачность предыдущих суток
            this._tableMain.Columns.Add(new DataColumn("ForecastCloudiness", typeof(int)));               //Прогноз облачности
            this._tableMain.Columns.Add(new DataColumn("ActualTemperaturePreviousDay", typeof(decimal)));  //Фактическая температура предыдущих суток
            this._tableMain.Columns.Add(new DataColumn("ForecastTemperature", typeof(decimal)));           //Прогноз температуры
            this._tableMain.Columns.Add(new DataColumn("CalculatedConsumptionForecast", typeof(decimal))); //Рассчитанный прогноз потребление 
            this._tableMain.Columns.Add(new DataColumn("ManualConsumptionForecast", typeof(decimal)));     //Ручной прогноз потребление 
            this._tableMain.Columns.Add(new DataColumn("ForecastConsumptionSO", typeof(decimal)));         //Прогноз потребления СО 

            //создаём таблицу с фактами            
            this._tableFact = new DataTable("TableFact");
            this._tableFact.Columns.Add(new DataColumn("Час", typeof(int))); //Час          

            //забиваем таблицы пустыми ячейками и часами от 1 до 24
            for (int i = 1; i <= 24; i++)
            {
                this._tableFact.Rows.Add(new object[] { i });
                this._tableMain.Rows.Add(new object[] { i });
            }

            //работа с ОИКом
            if (ckEnabled)
            {
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
                    //получаем данные ОИК по температуре
                    TI tiTemp = new TI(paramFackTempTI, paramFackTempCat);
                    OutData[] valueOIKTemp = ck.GetValue(tiTemp, this._dtPredDay, 60);
                    //получаем данные ОИК по потреблению
                    TI tiPotr = new TI(paramFackPotrTI, paramFackPotrCat);
                    OutData[] valueOIKPotr = ck.GetValue(tiPotr, this._dtPredDay, 60);

                    //записываем данные ОИК в таблицу    
                    for (int i = 1; i <= 24; i++)
                    {
                        DataRow row = this._tableMain.Rows[i - 1];
                        if (i < valueOIKTemp.Length && !valueOIKTemp[i].IsNoData)
                            row["ActualTemperaturePreviousDay"] = (decimal)Math.Round(valueOIKTemp[i].Value, 2);
                        if (i < valueOIKPotr.Length && !valueOIKPotr[i].IsNoData)
                            row["ActualConsumptionPreviousDay"] = (decimal)Math.Round(valueOIKPotr[i].Value, 2);
                    }

                    //получаем данные ОИК по потреблению за несколько предыдущих дней                    
                    for (DateTime dt = this._dtStartFact; dt < this._dtPredDay; dt = dt.AddDays(1))
                    {
                        //получаем данные ОИК по потреблению и записываем в таблицу
                        OutData[] valueOIKPotrPred = ck.GetValue(tiPotr, dt, 60);
                        this._tableFact.Columns.Add(new DataColumn(string.Format("{0} ({1})", DateUtils.DayMonthRPToStr(dt), DateUtils.DayOfWeekToRus(dt.DayOfWeek)), typeof(decimal)));
                        for (int i = 1; i <= 24; i++)
                        {
                            DataRow row = this._tableFact.Rows[i - 1];
                            if (i < valueOIKPotrPred.Length && !valueOIKPotrPred[i].IsNoData)
                                row[this._tableFact.Columns.Count - 1] = (decimal)Math.Round(valueOIKPotrPred[i].Value, 2);
                        }
                    }
                    ck.CloseConnection();
                }
            }

            //получение данных из макета прогноза потребления
            Dictionary<int, decimal> valueMaket1 = PlanPotrMaketClass.GetData(Vars.CON_STR, this._dtPrognoz, locationKPO, @"Values");
            Dictionary<int, decimal> valueMaket2 = PlanPotrMaketClass.GetData(Vars.CON_STR, this._dtPrognoz, locationKPO, @"SumValues");
            for (int i = 1; i <= 24; i++)
            {
                if (valueMaket1.ContainsKey(i))
                    this._tableMain.Rows[i - 1]["ForecastConsumptionSO"] = valueMaket1[i];
                if (valueMaket2.ContainsKey(i))
                    this._tableMain.Rows[i - 1]["ConsumptionMarketPlayers"] = valueMaket2[i];
            }
            valueMaket1.Clear();
            valueMaket2.Clear();


            //даные по прогнозу температуры

            //рассчет прогноза

        }

        /// <summary>нажатие клавиши обновить</summary>
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            this._dtPrognoz = new DateTime(this.dateTimePicker1.Value.Year, this.dateTimePicker1.Value.Month, this.dateTimePicker1.Value.Day, 0, 0, 0);
            this._dtPredDay = this._dtPrognoz.AddDays(-2);
            //определяем дату начала недели фактического потребления
            this._dtStartFact = this._dtPredDay;
            while (this._dtStartFact.DayOfWeek != DayOfWeek.Monday)
                this._dtStartFact = this._dtStartFact.AddDays(-1);
            this._dtStartFact = this._dtStartFact.AddDays(-7);

            //заполняем таблицы данными
            this.CalcAndFillTables();

            //биндим грид главного рассчета
            this.dataGridView1.Columns.Clear();
            string strDateColumn = Environment.NewLine + "на " + DateUtils.DayMonthRPToStr(this._dtPrognoz);
            string strPredDateColumn = Environment.NewLine + "на " + DateUtils.DayMonthRPToStr(this._dtPredDay);
            BindingSource bindingSource1 = new BindingSource();
            bindingSource1.DataSource = this._tableMain;
            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = bindingSource1;
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            this.dataGridView1.Columns[0].HeaderText = "Час";
            this.dataGridView1.Columns[0].Width = 40;
            this.dataGridView1.Columns[0].Frozen = true;
            this.dataGridView1.Columns[1].HeaderText = "Заявленное потребление субъектов рынка" + strDateColumn;
            this.dataGridView1.Columns[2].HeaderText = "Фактическое потребление" + strPredDateColumn;
            this.dataGridView1.Columns[3].HeaderText = "Облачность" + strPredDateColumn;
            this.dataGridView1.Columns[4].HeaderText = "Прогноз облачности" + strDateColumn;
            this.dataGridView1.Columns[5].HeaderText = "Фактическая температура" + strPredDateColumn;
            this.dataGridView1.Columns[6].HeaderText = "Прогноз температуры" + strDateColumn;
            this.dataGridView1.Columns[7].HeaderText = "Рассчитанный прогноз потребления" + strDateColumn;
            this.dataGridView1.Columns[8].HeaderText = "Ручной прогноз потребления" + strDateColumn;
            this.dataGridView1.Columns[9].HeaderText = "Прогноз потребления СО" + strDateColumn;
            for (int i = 1; i <= 9; i++)
            {
                //this.dataGridView1.Columns[i].DefaultCellStyle.Format = "N";
                this.dataGridView1.Columns[i].Width = 100;
                this.dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //биндим грид фактического потребления
            this.dataGridViewFact.Columns.Clear();
            BindingSource bindingSourceFact = new BindingSource();
            bindingSourceFact.DataSource = this._tableFact;
            this.dataGridViewFact.AutoGenerateColumns = true;
            this.dataGridViewFact.DataSource = bindingSourceFact;
            this.dataGridViewFact.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridViewFact.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            this.dataGridViewFact.Columns[0].Width = 40;
            this.dataGridViewFact.Columns[0].Frozen = true;
            for (int i = 1; i <= dataGridViewFact.Columns.Count - 1; i++)
            {
                //this.dataGridViewFact.Columns[i].DefaultCellStyle.Format = "N";
                this.dataGridViewFact.Columns[i].Width = 80;
                this.dataGridViewFact.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
        }

        /// <summary>Клавиша загрузки данных</summary>
        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            //диалог выбора файла
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Выберите макет для загрузки";
            dlg.Multiselect = true;
            dlg.Filter = "Файлы макета|*.xml|Все файлы|*.*";
            dlg.FilterIndex = 1;
            //dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                //todo : try except progress

                foreach (string maket in dlg.FileNames)
                {
                    if (!File.Exists(maket))
                    {
                        //progress.WriteComment("Не найден файл " + maket);
                        break;
                    }
                    StreamReader text = new StreamReader(maket);
                    PlanPotrMaketClass.Parse(Vars.CON_STR, text.ReadToEnd().Replace("encoding=\"windows-1251\"", "encoding=\"UTF-16\""));
                }
            }

            ButtonRefresh_Click(sender, e);

        }

        /// <summary>клавиша  убрать/показать фактические значения</summary>
        private void ButtonFactPotr_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2Collapsed = !this.splitContainer1.Panel2Collapsed;
        }

        /// <summary>Клавиша экспорт</summary>
        private void ButtonExportExcel_Click(object sender, EventArgs e)
        {
            //todo : try except progress
            mmr.share.Office.ExportToExcel(this.dataGridView1);

            //диалог выбора файла
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.Title = "Выберите файл для сохранения";
            //dlg.DefaultExt = "xls";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //    mmr.share.Office.ExportToExcelWithoutCOM(ds, dlg.FileName);
        }


    }
}
