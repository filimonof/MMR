using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mmr.share;

namespace mmr
{
    public partial class PPBR_CKParamForm : mmr.SimpleForm
    {
        public PPBR_CKParamForm()
        {
            InitializeComponent();
        }

        private void PPBR_CKParamForm_Load(object sender, EventArgs e)
        {
            //заполняем возможные кодировки
            this.comboBoxEncode.Items.Clear();
            this.comboBoxEncode.Items.AddRange(new object[] { EncodingMaket.Windows1251, EncodingMaket.UTF8, EncodingMaket.ASCII });
            this.comboBoxEncode.SelectedIndex = 0;

            this.tblPPBR_ParametersTableAdapter.Fill(this.dataSetPPBR.tblPPBR_Parameters);
            this.tblPPBR_CKParametersTableAdapter.Fill(this.dataSetPPBR.tblPPBR_CKParameters);

            this.ReloadOtherCKParam();
            this.Editing(false);
        }

        #region ReloadOtherCKParam
        public void ReloadOtherCKParam()
        {
            ParameterClass param = new ParameterClass(Vars.CON_STR);
            param.Open();
            this.textBoxDirectory.Text = param.Get(ParameterName.PPBR_CK_DirectoryOutput, string.Empty);
            this.textBoxFilename.Text = param.Get(ParameterName.PPBR_CK_FileName_Template, string.Empty);
            this.textBoxHeader.Text = param.Get(ParameterName.PPBR_CK_Header, string.Empty);
            this.textBoxEndData.Text = param.Get(ParameterName.PPBR_CK_EndData, string.Empty);
            this.textBoxSeparator.Text = param.Get(ParameterName.PPBR_CK_Separator, string.Empty);
            this.textBoxDecimalPoint.Text = param.Get(ParameterName.PPBR_CK_DecimalPoint, string.Empty);
            string encode = param.Get(ParameterName.PPBR_CK_Encode, string.Empty);
            if (encode != string.Empty)
                this.comboBoxEncode.Text = encode;
        }
        #endregion

        #region SaveOtherCKParam
        public void SaveOtherCKParam()
        {
            ParameterClass param = new ParameterClass(Vars.CON_STR);
            param.Set(ParameterName.PPBR_CK_DirectoryOutput, this.textBoxDirectory.Text);
            param.Set(ParameterName.PPBR_CK_FileName_Template, this.textBoxFilename.Text);
            param.Set(ParameterName.PPBR_CK_Header, this.textBoxHeader.Text);
            param.Set(ParameterName.PPBR_CK_EndData, this.textBoxEndData.Text);
            param.Set(ParameterName.PPBR_CK_Separator, this.textBoxSeparator.Text);
            param.Set(ParameterName.PPBR_CK_Encode, this.comboBoxEncode.Text);
            param.Set(ParameterName.PPBR_CK_DecimalPoint, this.textBoxDecimalPoint.Text);
        }
        #endregion

        #region Организауия сохранения данных  Editing Save Cancel Close TextChanged
        private void Editing(bool ugu)
        {
            this.buttonSave.Enabled = ugu;
            this.buttonCancel.Enabled = ugu;
            this.buttonClose.Enabled = !ugu;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this.gridViewCKParam.EndEdit(); 
            //dataGrid.EndEdit(null, dataGrid.CurrentRowIndex, false);
            //dataGrid.CurrentRowIndex = dataGrid.CurrentRowIndex;

            if (this.dataSetPPBR.tblPPBR_CKParameters.GetChanges() != null)
                this.tblPPBR_CKParametersTableAdapter.Update(this.dataSetPPBR);
            this.tblPPBR_CKParametersTableAdapter.Fill(this.dataSetPPBR.tblPPBR_CKParameters);

            this.SaveOtherCKParam();
            this.Editing(false);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.gridViewCKParam.CancelEdit();            
            this.dataSetPPBR.RejectChanges();
            this.tblPPBR_CKParametersTableAdapter.Fill(this.dataSetPPBR.tblPPBR_CKParameters);

            this.ReloadOtherCKParam();
            this.Editing(false);
        }

        private void Parameters_TextChanged(object sender, EventArgs e)
        {
            this.Editing(true);
        }
        #endregion

        #region выбор директории
        private void buttonSelectDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            dlg.SelectedPath = this.textBoxDirectory.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
                this.textBoxDirectory.Text = dlg.SelectedPath;
        }
        #endregion

        //todo : переделать навигатор

        private void gridViewCKParam_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            //проверка введённых данныех

            /*
            switch (this.gridViewCKParam.Columns[e.ColumnIndex].Name)
            {
                case "parameterIDDataGridViewTextBoxColumn":
                    if (this.gridViewCKParam[e.ColumnIndex, e.RowIndex].Value == null)
                    {
                        MessageBox.Show("Не введён параметр");
                        e.Cancel = true;
                    }
                    break;

                case "controlSummaDataGridViewCheckBoxColumn":
                    e.Cancel = true;
                    break;
            } 
             */
        }

        private void gridViewCKParam_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //ячейка ожидает значения - для невинденых значений
            /*
            if (this.gridViewCKParam.Columns[e.ColumnIndex].Name == "controlSummaDataGridViewCheckBoxColumn")
            {
                e.Value = 1;
            }
             */
        }

        private void gridViewCKParam_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridViewCKParam_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.Editing(true);
        }

        private void gridViewCKParam_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.Editing(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Editing(true);
            this.tblPPBRCKParametersBindingSource.AddNew();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Editing(true);
            this.tblPPBRCKParametersBindingSource.RemoveCurrent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Editing(true);
            this.gridViewCKParam.BeginEdit(true);
            //this.gridViewCKParam.CommitEdit

        }


        /*
ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.en/dv_fxmclictl/html/74eb5276-5ab8-4ce0-8005-dae751d85f7c.htm
         
CellValueNeeded
  Используется контроля для получения клеток значения из кэш-памяти данных для отображения. Это событие происходит только на клетки в несвязанных столбцов.
 
CellValuePushed
  Используется контроля совершить пользователю для ввода ячейки с данными кэша. Это событие происходит только на клетки в несвязанных столбцов.

Позвоните UpdateCellValue метода при изменении кэшированный стоимость за один CellValuePushed события, чтобы текущее значение отображается на контроль и применять какие-либо автоматической калибровки режимов в настоящее время в силу.
 
NewRowNeeded
  Используется для контроля свидетельствуют о необходимости создания новой строки в кэше данных.
 
RowDirtyStateNeeded
  Используется контроля для определения того, является ли строка есть свободные изменения.
 
CancelRowEdit
  Используется для контроля свидетельствуют о том, что строка должна вернуться к своей кэшированные значения.
          
RowValidated 	
    Это событие является аналогом одобренный событие. Используйте это событие для выполнения пост-обработки на ряд значений.           
        */
    }
}
