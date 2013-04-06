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
    public partial class PPBREmailForm : Form
    {
        /// <summary>Инициализация</summary>
        public PPBREmailForm()
        {
            InitializeComponent();
            this.ReloadParam();
            this.Editing(false);
        }

        #region ReloadParam
        /// <summary>Получение параметров</summary>
        public void ReloadParam()
        {
            ParameterClass param = new ParameterClass(Vars.CON_STR);
            param.Open();
            this.textBoxSMTP.Text = param.Get(ParameterName.PPBR_SMTP_Server, string.Empty);
            this.textBoxSMTPPort.Text = param.Get(ParameterName.PPBR_SMTP_Port, string.Empty);
            
            this.textBoxPOP.Text = param.Get(ParameterName.PPBR_POP3_Server, string.Empty);
            this.textBoxPOPPort.Text = param.Get(ParameterName.PPBR_POP3_Port, string.Empty);
            this.textBoxEmail.Text = param.Get(ParameterName.PPBR_Email, string.Empty);
            this.textBoxPas.Text = param.Get(ParameterName.PPBR_Email_Password, string.Empty);
            this.checkBoxIsDeleted.Checked = param.Get(ParameterName.PPBR_Email_DeletedAfterLoad, false);

            this.textBoxTimeOut.Text = param.Get(ParameterName.PPBR_Email_Timeout, string.Empty);
            this.textBoxSubject.Text = param.Get(ParameterName.PPBR_Email_Subject, string.Empty);
            
            this.textBoxReply.Text = param.Get(ParameterName.PPBR_Email_Replay, string.Empty);
            this.textBoxSender.Text = param.Get(ParameterName.PPBR_Email_Sender, string.Empty);       
        }
        #endregion

        #region SaveParam
        /// <summary>сохранение параметров</summary>
        public void SaveParam()
        {
            ParameterClass param = new ParameterClass(Vars.CON_STR);
            param.Set(ParameterName.PPBR_SMTP_Server, this.textBoxSMTP.Text);
            param.Set(ParameterName.PPBR_SMTP_Port, this.textBoxSMTPPort.Text);

            param.Set(ParameterName.PPBR_POP3_Server, this.textBoxPOP.Text);
            param.Set(ParameterName.PPBR_POP3_Port, this.textBoxPOPPort.Text);
            param.Set(ParameterName.PPBR_Email, this.textBoxEmail.Text);
            param.Set(ParameterName.PPBR_Email_Password, this.textBoxPas.Text);
            param.Set(ParameterName.PPBR_Email_DeletedAfterLoad, this.checkBoxIsDeleted.Checked);

            param.Set(ParameterName.PPBR_Email_Timeout, this.textBoxTimeOut.Text);
            param.Set(ParameterName.PPBR_Email_Subject, this.textBoxSubject.Text);
            
            param.Set(ParameterName.PPBR_Email_Replay, this.textBoxReply.Text);
            param.Set(ParameterName.PPBR_Email_Sender, this.textBoxSender.Text);
        }
        #endregion

        #region Организация сохранения данных Editing Close Save Cancel TextChanged
        /// <summary>данные редактировались</summary>
        /// <param name="ugu">true - если данные изменены</param>
        private void Editing(bool ugu)
        {
            this.ButtonSave.Enabled = ugu;
            this.ButtonCancel.Enabled = ugu;
            this.ButtonClose.Enabled = !ugu;
        }

        /// <summary>закрыть форму</summary>
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>сохранить</summary>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this.SaveParam();
            this.Editing(false);
        }

        /// <summary>отменить</summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.ReloadParam();
            this.Editing(false);
        }

        /// <summary>событие изменения текста</summary>
        private void Parameters_TextChanged(object sender, EventArgs e)
        {
            this.Editing(true);
        }
        #endregion

    }
}
