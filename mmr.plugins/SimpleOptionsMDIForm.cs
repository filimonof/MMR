using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mmr.plugins
{
    public partial class SimpleOptionsMDIForm : SimpleMDIForm, IOptionsMDIForm
    {
        /// <summary>конструктор</summary>
        public SimpleOptionsMDIForm()
        {
            InitializeComponent();
        }

        /// <summary>Загрузка формы</summary>
        private void SimpleOptionsMDIForm_Load(object sender, EventArgs e)
        {
            vit.progress.WaitingDlg.ShowWaitingDlg();               
            try
            {                
                if (((IOptionsMDIForm)this).Icon != null)
                    this.Icon = ((IOptionsMDIForm)this).Icon;
                ((IOptionsMDIForm)this).LoadData();
                this.Editing(false);
            }
            finally
            {
                vit.progress.WaitingDlg.CloseWaitingDlg();                
            }
        }

        #region реализация IOptionsMDIForm
        /// <summary>Название формы</summary>        
        string IPluginsToMenu.NameForm
        {
            // должно переопределиться в наследниках
            get { return @"SimpleOptionsMDIForm"; }
        }

        /// <summary>Родительский пункт меню</summary>        
        string IPluginsToMenu.ParentMenu
        {
            // должно переопределиться в наследниках
            get { return @"ParentNameMenu"; }
        }

        /// <summary>Иконка</summary>        
        Icon IPluginsToMenu.Icon
        {
            // должно переопределиться в наследниках
            get { return null; }
        }

        /// <summary>Загрузка данных</summary>
        void IOptionsMDIForm.LoadData()
        {
            // должно переопределиться в наследниках
            this.Editing(false);
        }

        /// <summary>сохранение данных</summary>
        void IOptionsMDIForm.SaveData()
        {
            // должно переопределиться в наследниках
            this.Editing(false);
        }
        #endregion

        /// <summary>событие - изменение настроек</summary>
        protected void Parameters_Changed(object sender, EventArgs e)
        {
            this.Editing(true);
        }

        /// <summary>данные редактировались</summary>
        /// <param name="ugu">true - если данные изменены</param>
        public void Editing(bool ugu)
        {
            this.toolStripButtonSave.Enabled = ugu;
            this.toolStripButtonCancel.Enabled = ugu;
            this.toolStripButtonClose.Enabled = !ugu;
        }

        /// <summary>нажато Сохранение</summary>
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            vit.progress.WaitingDlg.ShowWaitingDlg(@"Сохранение данных");
            try
            {                
                ((IOptionsMDIForm)this).SaveData();
                this.Editing(false);
            }
            finally
            {
                vit.progress.WaitingDlg.CloseWaitingDlg();
            }
        }

        /// <summary>нажато Отмена</summary>
        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            vit.progress.WaitingDlg.ShowWaitingDlg(@"Отмена сохранения");
            try
            {                
                ((IOptionsMDIForm)this).LoadData();
                this.Editing(false);
            }
            finally
            {
                vit.progress.WaitingDlg.CloseWaitingDlg();
            }
        }

        /// <summary>нажато Закрыть</summary>
        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
