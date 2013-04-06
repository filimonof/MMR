using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mmr.share;
using vit.dialogs;

namespace mmr
{
    public partial class PPBRParamForm : Form
    {
        private PPBRParamClass _paramPPBR;
        private List<int> _paramID;
        private List<int> _paramArhID;
        private const string STR_LABELTIME = " (метка времени)";

        #region PPBRParamForm() - конструктор формы параметров ППБР
        /// <summary>
        /// конструктор формы параметров ППБР
        /// </summary>
        public PPBRParamForm()
        {
            InitializeComponent();
            this._paramPPBR = new PPBRParamClass();
            //this._paramPPBR = new PPBRParamClass(Properties.Settings.Default.mmr_dbConnectionString);
            this._paramID = new List<int>();
            this._paramArhID = new List<int>();
        }
        #endregion

        #region PPBRParamForm_Load - загрузка формы
        private void PPBRParamForm_Load(object sender, EventArgs e)
        {
            //заполняем возможные кодировки
            this.comboBoxEncode.Items.Clear();
            this.comboBoxEncode.Items.AddRange(new object[] { EncodingMaket.Windows1251, EncodingMaket.UTF8, EncodingMaket.ASCII });
            this.comboBoxEncode.SelectedIndex = 0;

            this.ReloadWorkParam();
            this.ReloadArhiveParam();
            this.ReloadOtherParam();
            this.Editing(false);
        }
        #endregion

        #region ReloadWorkParam
        public void ReloadWorkParam() { this.ReloadWorkParam(0); }
        public void ReloadWorkParam(int offsetSelect)
        {
            //вывести из PPBRParamClass param  на форму            
            int select = (this.listBoxWork.SelectedIndex != -1) ? this.listBoxWork.SelectedIndex : 0;
            this.listBoxWork.Items.Clear();
            this._paramID.Clear();
            foreach (DataRow dr in this._paramPPBR.GetWorkParam())
            {
                this.listBoxWork.Items.Add(dr["Name"] + ((bool)dr["IsLabelTime"] ? STR_LABELTIME : string.Empty));
                this._paramID.Add((int)dr["ID"]);
            }
            select = select + offsetSelect;
            if (select < this.listBoxWork.Items.Count) this.listBoxWork.SelectedIndex = select;
        }
        #endregion

        #region ReloadArhiveParam
        public void ReloadArhiveParam() { this.ReloadArhiveParam(0); }
        public void ReloadArhiveParam(int offsetSelect)
        {
            int select = (this.listBoxArh.SelectedIndex != -1) ? this.listBoxArh.SelectedIndex : 0;
            this.listBoxArh.Items.Clear();
            this._paramArhID.Clear();
            foreach (DataRow dr in this._paramPPBR.GetArhiveParam())
            {
                this.listBoxArh.Items.Add(dr["Name"]);
                this._paramArhID.Add((int)dr["ID"]);
            }
            select = select + offsetSelect;
            if (select < this.listBoxArh.Items.Count) this.listBoxArh.SelectedIndex = select;
        }
        #endregion

        #region ReloadOtherParam
        public void ReloadOtherParam()
        {
            ParameterClass param = new ParameterClass(Vars.CON_STR);
            param.Open();
            this.textBoxFilename.Text = param.Get(ParameterName.PPBR_FileName_Template, string.Empty);
            this.textBoxHead.Text = param.Get(ParameterName.PPBR_Header, string.Empty);
            this.textBoxBeginData.Text = param.Get(ParameterName.PPBR_BeginData, 0).ToString();
            this.textBoxSeparator.Text = param.Get(ParameterName.PPBR_Separator, string.Empty);
            this.textBoxDecimalPoint.Text = param.Get(ParameterName.PPBR_DecimalPoint, string.Empty);
            string encode = param.Get(ParameterName.PPBR_Encode, string.Empty);
            if (encode != string.Empty)
                this.comboBoxEncode.Text = encode;
        }
        #endregion

        #region SaveOtherParam
        public void SaveOtherParam()
        {
            ParameterClass param = new ParameterClass(Vars.CON_STR);
            param.Set(ParameterName.PPBR_FileName_Template, this.textBoxFilename.Text);
            param.Set(ParameterName.PPBR_Encode, this.comboBoxEncode.Text);
            param.Set(ParameterName.PPBR_Header, this.textBoxHead.Text);
            param.Set(ParameterName.PPBR_BeginData, this.textBoxBeginData.Text);
            param.Set(ParameterName.PPBR_Separator, this.textBoxSeparator.Text);
            param.Set(ParameterName.PPBR_DecimalPoint, this.textBoxDecimalPoint.Text);
        }
        #endregion

        #region Buttons - Save Cancel Close
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this.SaveOtherParam();
            this._paramPPBR.Save();

            this.ReloadWorkParam();
            this.ReloadArhiveParam();
            this.Editing(false);           
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.ReloadOtherParam();
            this._paramPPBR.Cancel();

            this.ReloadWorkParam();
            this.ReloadArhiveParam();
            this.Editing(false);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {            
            this.Close();
        }
        #endregion

        #region Buttons - Add Del Rename LabelTime
        private void buttonAdd_Click(object sender, EventArgs e)
        {            
            //new parameter
            string newParam = "";
            if (InputStringDlg.GetString("Введите имя параметра для макета ППБР", ref newParam))
            {
                //проверка есть ли такое имя, или на триггер
                //поймать exception
                this._paramPPBR.AddParam(newParam);
                this.ReloadWorkParam();
                //this.Editing(true);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            //del                       
            if (this.listBoxWork.SelectedIndex == -1)
            {
                MessageBox.Show("Не выделен ни один параметр", "Удаление данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show(string.Format("Желаете удалить параметр \"{0}\"? \nВсе сохраненые данные по этому параметру будут удалены!!!", this.listBoxWork.SelectedItem.ToString())
                    , "Удаление данных", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    //поймать exception                    
                    this._paramPPBR.DelParam(this._paramID[this.listBoxWork.SelectedIndex]);
                    this.ReloadWorkParam();
                    //this.Editing(true);
                }
            }
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            //rename parameter
            if (this.listBoxWork.SelectedIndex == -1)
            {
                MessageBox.Show("Не выделен ни один параметр", "Переименовывание параметра", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string newParam = this.listBoxWork.SelectedItem.ToString();
                int i = newParam.IndexOf(STR_LABELTIME);
                if (i > 0) newParam = newParam.Remove(i);
                if (InputStringDlg.GetString(string.Format("Введите новое имя параметра \n \"{0}\"", newParam), ref newParam))
                {
                    //exception                    
                    this._paramPPBR.RenameParam(this._paramID[this.listBoxWork.SelectedIndex], newParam);
                    this.ReloadWorkParam();
                    //this.Editing(true);
                }
            }
        }

        private void buttonMakeLabelTime_Click(object sender, EventArgs e)
        {
            //time
            if (this.listBoxWork.SelectedIndex == -1)
            {
                MessageBox.Show("Не выделен ни один параметр", "Установка метки времени", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //exception
                this._paramPPBR.MakeLabelTime(this._paramID[this.listBoxWork.SelectedIndex]);
                this.ReloadWorkParam();
                //this.Editing(true);
            }
        }
        #endregion

        #region Buttons - Up Down
        private void buttonUp_Click(object sender, EventArgs e)
        {
            //up
            if (this.listBoxWork.SelectedIndex == -1)
            {
                MessageBox.Show("Не выделен ни один параметр", "Перемещение параметра вверх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (this.listBoxWork.SelectedIndex > 0)
                {
                    //exception
                    this._paramPPBR.Up(this._paramID[this.listBoxWork.SelectedIndex]);
                    this.ReloadWorkParam(-1);
                    //this.Editing(true);
                }
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            //down
            if (this.listBoxWork.SelectedIndex == -1)
            {
                MessageBox.Show("Не выделен ни один параметр", "Перемещение параметра вниз", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (this.listBoxWork.SelectedIndex < this.listBoxWork.Items.Count - 1)
                {
                    //exception
                    this._paramPPBR.Down(this._paramID[this.listBoxWork.SelectedIndex]);
                    this.ReloadWorkParam(+1);
                    //this.Editing(true);
                }
            }
        }
        #endregion

        #region Buttons - InArh InWork
        private void buttonInArh_Click(object sender, EventArgs e)
        {
            //arhive
            if (this.listBoxWork.SelectedIndex == -1)
            {
                MessageBox.Show("Не выделен ни один параметр", "Перемещение в архив", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //exception
                this._paramPPBR.Disabled(this._paramID[this.listBoxWork.SelectedIndex]);
                this.ReloadWorkParam(-1);
                this.ReloadArhiveParam();
                //this.Editing(true);
            }
        }

        private void buttonInWork_Click(object sender, EventArgs e)
        {
            //work
            if (this.listBoxArh.SelectedIndex == -1)
            {
                MessageBox.Show("Не выделен ни один параметр", "Перемещение в работу", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //exception
                this._paramPPBR.Enabled(this._paramArhID[this.listBoxArh.SelectedIndex]);
                this.ReloadWorkParam();
                this.ReloadArhiveParam(-1);
                //this.Editing(true);
            }
        }
        #endregion

        #region изменения данных
        private void Editing(bool ugu)
        {
            this.ButtonSave.Enabled = ugu;
            this.ButtonCancel.Enabled = ugu;
            this.ButtonClose.Enabled = !ugu;
        }

        private void Parameters_TextChanged(object sender, EventArgs e)
        {
            this.Editing(true);
        }
        #endregion

    }
}
