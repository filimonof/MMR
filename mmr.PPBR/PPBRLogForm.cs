using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using vit.progress;
using mmr.share;
using mmr.share.PPBR;

namespace mmr
{
    public partial class PPBRLogForm : mmr.SimpleForm
    {
        #region PPBRForm() - конструктор формы ППБР
        /// <summary>
        /// конструктор формы ППБР
        /// </summary>
        public PPBRLogForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Load - загрузка формы
        private void PPBRLogForm_Load(object sender, EventArgs e)
        {
            //отключить обновление
            this.ComboBoxDate.BeginUpdate();
            this.ComboBoxMonth.BeginUpdate();
            this.ComboBoxYear.BeginUpdate();

            //заполнить месяца
            this.ComboBoxMonth.Items.Clear();
            this.ComboBoxMonth.Items.AddRange(DateUtils.NameMonthRus);
            this.ComboBoxMonth.SelectedIndex = DateTime.Today.Month - 1;

            //заполнить месяца
            this.ComboBoxDate.Items.Clear();
            this.ComboBoxDate.Items.AddRange(new string[] { "Макеты", "События" });
            this.ComboBoxDate.SelectedIndex = 0;

            //заполнить годы            
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["mmr.Properties.Settings.mmr_dbConnectionString"].ConnectionString))
            {
                string QUERY_YEARS =
                    " SELECT DISTINCT YEAR(DateMaket) AS YearMaket "
                    + " FROM tblPPBR_Logs "
                    + " ORDER BY YearMaket ";
                SqlCommand command = new SqlCommand(QUERY_YEARS, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                this.ComboBoxYear.Items.Clear();
                while (reader.Read())
                    this.ComboBoxYear.Items.Add(reader[0]);
            }
            if (ComboBoxYear.Items.IndexOf(DateTime.Today.Year) == -1)
                ComboBoxYear.Items.Add(DateTime.Today.Year);
            ComboBoxYear.SelectedItem = DateTime.Today.Year;

            //включить обновдение
            this.ComboBoxDate.EndUpdate();
            this.ComboBoxMonth.EndUpdate();
            this.ComboBoxYear.EndUpdate();
        }
        #endregion

        #region Reload() - обновить данные
        public void Reload(int month, int? year)
        {
            this.Reload(month, year, false);
        }

        /// <summary>
        /// обновить данные
        /// </summary>
        public void Reload(int month, int? year, bool isSelect)
        {
            //todo : сделать запоминание состояния курсора
            /*
             int select = (this.listBoxWork.SelectedIndex != -1) ? this.listBoxWork.SelectedIndex : 0;
             *             if (select < this.listBoxWork.Items.Count) this.listBoxWork.SelectedIndex = select;

             */
            if (year.HasValue)
            {
                DateTime dt1 = new DateTime((int)year, month, 1);
                DateTime dt2 = dt1.AddMonths(1).AddDays(-1);

                //List<int> sel = new List<int>();
                int cur = -1;
                if (isSelect)
                {
                    //выделеные                    
                    //if (this.gridLogsPPBR.SelectedRows.Count > 0)
                    //{
                    //    for (int i = 0; i < this.gridLogsPPBR.SelectedRows.Count; i++)
                    //        sel.Add(this.gridLogsPPBR.SelectedRows[i].Index);
                    //}

                    //текущий                    
                    if (this.gridLogsPPBR.CurrentRow != null)
                        cur = this.gridLogsPPBR.CurrentRow.Index;
                    DataGridViewRow dr = this.gridLogsPPBR.CurrentRow;
                }

                if (this.ComboBoxDate.SelectedIndex == 0)
                    this.tblPPBR_LogsTableAdapter.Fill(this.dataSetPPBR.tblPPBR_Logs, dt1, dt2);
                else
                    this.tblPPBR_LogsTableAdapter.FillByEvent(this.dataSetPPBR.tblPPBR_Logs, dt1, dt2);

                if (isSelect)
                {
                    if (cur != -1)
                        this.tblPPBRLogsBindingSource.Position = cur;
                    //выделеные
                    //if (sel.Count > 0)
                    //{
                    //    for (int i = 0; i < sel.Count; i++)
                    //        this.gridLogsPPBR.Rows[sel[i]].Selected = true;
                    //}

                }
            }
        }
        #endregion

        #region Собятия меню обновления данных
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem);
        }

        private void ComboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem);
        }

        private void ComboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem);
        }

        private void ComboBoxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem);
        }
        #endregion

        #region Загрузка данных из почты
        /// <summary>
        /// загрузка из почты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAutoLoad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Желаете загрузить днные из почты?", "Загрузка данных"
                 , MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                return;

            ProgressDlg progress = new ProgressDlg("Загрузка макетов ППБР в базу", true);
            progress.Show();
            progress.ButtonDisabled();
            EmailClass_Get mail = new EmailClass_Get(progress);
            try
            {
                mail.GetMail(Vars.CON_STR);
            }
            catch (EmailException Ex)
            {
                progress.WriteComment(Ex.Message);
                progress.WriteComment("Макет не создан");                                
                Log.ToFile(string.Format("Ошибка при создании макета во время загрузки из почты: \n {0}", Ex.Message));
            }
            catch (Exception Ex)
            {
                progress.WriteComment("Произошла ошибка при загрузке макета ППБР в базу из почты: \n" + Ex.Message);
                progress.WriteComment("Загрузка отменена");
                Log.ToFile(string.Format("Ошибка при создании макета во время загрузки из почты: \n {0}", Ex.Message));
            }
            finally
            {
                mail.Dispose();
                progress.ButtonEnabled();
            }
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem, true);
        }
        #endregion

        #region Загрузка данных с жесткого диска
        /// <summary>
        /// загрузка с жёсткого диска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuLoad_Click(object sender, EventArgs e)
        {
            //получаем шаблон имени файла
            string templateFileName = ParameterClass.GetParam(Vars.CON_STR, ParameterName.PPBR_FileName_Template, string.Empty);
            //меняем {..} на *
            templateFileName = Regex.Replace(templateFileName, @"\{\S*\}", "*");
            //диалог выбора файла
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Выберите макет для загрузки";
            dlg.Multiselect = true;
            dlg.Filter = string.Format("Файлы макета ({0:s})|{0:s}|Все файлы|*.*", templateFileName);
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ProgressDlg progress = new ProgressDlg("Загрузка макетов ППБР в базу", true);
                progress.Show();
                progress.ButtonDisabled();
                //оценим сколько будет NEXT у ProgressBar
                progress.CountNext = dlg.FileNames.Length;
                foreach (string maket in dlg.FileNames)
                {
                    if (!File.Exists(maket))
                    {
                        progress.WriteComment("Не найден файл " + maket);
                        break;
                    }

                    progress.WriteComment("Загрузка макета " + maket);

                    progress.WriteComment("Запись в лог события");
                    int logID = 0;
                    try
                    {
                        //извлекаем дату из файла
                        DateTime dateInFile = MaketClass.ParseDateInMaket(Vars.CON_STR, maket);
                        //записываем событие в лог
                        logID = LogClass.Add(Vars.CON_STR, dateInFile);
                        progress.WriteComment(string.Format("Событие №{0} от {1}", logID, dateInFile.ToShortDateString()));
                    }
                    catch (Exception Ex)
                    {
                        progress.WriteComment(string.Format("Произошла ошибка при записи события: \n {0}", Ex.Message));
                        progress.WriteComment("Загрузка отменена");
                        Log.ToFile(string.Format("Произошла ошибка при записи события: \n {0}", Ex.Message));
                        //progress.IsError = true;
                        break;
                    }

                    MaketClass parse = new MaketClass(Vars.CON_STR, maket, logID, progress);
                    try
                    {
                        parse.Parse();
                    }
                    catch (MaketException Ex)
                    {
                        progress.WriteComment(Ex.Message);
                        progress.WriteComment("Загрузка отменена");
                        Log.ToFile(string.Format("Произошла ошибка во время парсинга макета: \n {0}", Ex.Message));
                        parse.ParseRollback(Ex.Message);
                    }
                    catch (Exception Ex)
                    {
                        string error = string.Format("Произошла ошибка при загрузке файла макета ППБР \"{0}\" в базу: \n {1}", maket, Ex.Message);
                        progress.WriteComment(error);
                        progress.WriteComment("Загрузка отменена");
                        Log.ToFile(error);
                        parse.ParseRollback(error);
                    }
                    finally
                    {
                        parse.Dispose();
                    }
                    progress.WriteComment("");
                    progress.Next();
                }
                //if (!progress.IsError || !progress.Cancelled) progress.Close();
                progress.ButtonEnabled();
            }
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem, true);
        }
        #endregion

        #region Просмотр данных
        private void ButtonShowData_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Закрытие формы
        private void ButtonClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Выгрузка данных в СК
        private void MenuMaketCK_Click(object sender, EventArgs e)
        {
            if (this.gridLogsPPBR.SelectedRows.Count < 1)
            {
                MessageBox.Show("Не выделены данные для отправки.", "Загрузка данных в СК", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Желаете отправить данные ППБР в СК-2003?", "Выгрузка данных в СК-2003"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                return;

            ProgressDlg progress = new ProgressDlg("Создание макета для СК-2003", true);
            progress.Show();
            progress.ButtonDisabled();
            //оценим сколько будет NEXT у ProgressBar
            progress.CountNext = this.gridLogsPPBR.SelectedRows.Count;
            foreach (DataGridViewRow row in this.gridLogsPPBR.SelectedRows)
            {

                int logID = (int)row.Cells["iDDataGridViewTextBoxColumn"].Value;

                //---

                this.UploadCK(logID, progress);

                //----

                progress.WriteComment("");
                progress.Next();
            }
            progress.ButtonEnabled();
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem, true);
        }

        private void UploadCK(int logID, ProgressDlg progress)
        {
            string error_code = string.Empty;
            progress.WriteComment(string.Format("Создание макета для СК по ППБР №{0}", logID));
            CKMaketClass maket = new CKMaketClass(Vars.CON_STR, logID, progress);
            try
            {
                maket.CreateMaket();
                try
                {
                    LogClass.Add(Vars.CON_STR, maket.DateMaket, StatusLog.UploadCK_OK, null);
                    progress.WriteComment(string.Format("Запись в лог об успешной отправке данный ППБР №{0} от {1} в СК", logID, maket.DateMaket.ToShortDateString()));
                }
                catch { }
            }
            catch (CKMaketException Ex)
            {
                progress.WriteComment(Ex.Message);
                progress.WriteComment("Макет не создан");
                Log.ToFile(string.Format("Ошибка при выгрузке макета для СК: \n {0}", Ex.Message));
                error_code = Ex.Message;
            }
            catch (Exception Ex)
            {
                string error = string.Format("Произошла ошибка при создании макета для СК-2003: \n {0}", Ex.Message);
                progress.WriteComment(error);
                progress.WriteComment("Макет не создан");
                Log.ToFile(string.Format("Ошибка при выгрузке макета для СК: \n {0}", Ex.Message));
                error_code = Ex.Message;
            }
            finally
            {
                maket.Dispose();
                if (error_code != string.Empty)
                {
                    try
                    {
                        //записываем ошибку в лог
                        LogClass.Add(Vars.CON_STR, maket.DateMaket, StatusLog.UploadCK_Error, error_code);
                        progress.WriteComment(string.Format("Ошибка об отправке данных ППБР №{0} от {1} в СК-2003 записана в лог", logID, maket.DateMaket.ToShortDateString()));
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region Пересылка макета ППБР
        private void MenuReplay_Click(object sender, EventArgs e)
        {
            if (this.gridLogsPPBR.SelectedRows.Count < 1)
            {
                MessageBox.Show("Не выделены данные для отправки.", "Пересылка макета ППБР", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Желаете переслать ППБР адресатам?", "Пересылка макета ППБР"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                return;

            ProgressDlg progress = new ProgressDlg("Создание макета для СК-2003", true);
            progress.Show();
            progress.ButtonDisabled();
            //оценим сколько будет NEXT у ProgressBar
            progress.CountNext = this.gridLogsPPBR.SelectedRows.Count;
            foreach (DataGridViewRow row in this.gridLogsPPBR.SelectedRows)
            {
                int logID = (int)row.Cells["iDDataGridViewTextBoxColumn"].Value;

                //----

                this.ReplyMaket(logID, progress);

                // ---

                progress.WriteComment("");
                progress.Next();
            }
            progress.ButtonEnabled();
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem, true);
        }

        private void ReplyMaket(int logID, ProgressDlg progress)
        {
            string error_code = string.Empty;
            progress.WriteComment(string.Format("Пересылка макета ППБР №{0}", logID));
            MaketClass maket = new MaketClass(Vars.CON_STR, logID, progress);
            try
            {
                maket.CreateMaket();
                string fileMaket = maket.FileName;
                List<string> fileList = new List<string>();
                fileList.Add(fileMaket);
                progress.WriteComment("Отправка макета по почте");
                EmailClass_Send.SendMail(Vars.CON_STR, fileList);
                try
                {
                    File.Delete(fileMaket);
                    LogClass.Add(Vars.CON_STR, maket.DateMaket, StatusLog.SendPPBR_OK, null);
                    progress.WriteComment(string.Format("Запись в лог об успешной пересылке макета ППБР №{0} от {1}", logID, maket.DateMaket.ToShortDateString()));
                }
                catch { }
            }
            catch (MaketException Ex)
            {
                progress.WriteComment(Ex.Message);
                progress.WriteComment("Макет не создан");
                Log.ToFile(string.Format("Произошла ошибка при создании/пересылки макета ППБР: \n {0}", Ex.Message));
                error_code = Ex.Message;
            }
            catch (Exception Ex)
            {
                string error = string.Format("Произошла ошибка при создании/пересылки макета ППБР: \n {0}", Ex.Message);
                progress.WriteComment(error);
                progress.WriteComment("Макет не создан");
                Log.ToFile(error);
                error_code = Ex.Message;
            }
            finally
            {
                maket.Dispose();
                if (error_code != string.Empty)
                {
                    try
                    {
                        //записываем ошибку в лог
                        LogClass.Add(Vars.CON_STR, maket.DateMaket, StatusLog.SendPPBR_Error, error_code);
                        progress.WriteComment(string.Format("Ошибка об отправке макета ППБР №{0} от {1} по почте записана в лог", logID, maket.DateMaket.ToShortDateString()));
                    }
                    catch { }
                }
            }
        }

        #endregion

        #region Весь цикл обработки
        private void MenyDownLoadAndUpload_ButtonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Желаете проверить почту и выгрузить макеты?", "Макет ППБР"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                return;

            ProgressDlg progress = new ProgressDlg("Макеты ППБР", true);
            progress.Show();
            progress.ButtonDisabled();
            try
            {
                EmailClass_Get mail = new EmailClass_Get(progress);
                try
                {
                    mail.GetMail(Vars.CON_STR);
                    List<int> logsID = mail.GetTryLoadedID();
                    foreach (int log in logsID)
                    {
                        //пересылаем
                        this.ReplyMaket(log, progress);
                    }
                    foreach (int log in logsID)
                    {
                        //выгружаем в АИК
                        this.UploadCK(log, progress);
                    }
                }
                catch (EmailException Ex)
                {
                    progress.WriteComment(Ex.Message);
                    progress.WriteComment("Макет не создан");
                    Log.ToFile(string.Format("Ошибка при обработке макета: \n {0}", Ex.Message));                    
                }
                catch (Exception Ex)
                {
                    progress.WriteComment("Произошла ошибка при загрузке макета ППБР в базу из почты: \n" + Ex.Message);
                    progress.WriteComment("Загрузка отменена");
                    Log.ToFile(string.Format("Произошла ошибка при загрузке макета ППБР в базу из почты: \n {0}", Ex.Message));                    
                }
                finally
                {
                    mail.Dispose();
                }
            }
            finally
            {
                progress.ButtonEnabled();
            }
            this.Reload(ComboBoxMonth.SelectedIndex + 1, (int?)ComboBoxYear.SelectedItem, true);
        }
        #endregion



    }

}
