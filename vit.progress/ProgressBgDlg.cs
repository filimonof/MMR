using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vit.progress
{
    /// <summary>
    /// Состояние прогресса
    /// </summary>
    public enum ProgressBgState
    {
        /// <summary>
        /// идет процесс
        /// </summary>
        Progress,

        /// <summary>
        /// отменяется процесс
        /// </summary>
        Cancel,

        /// <summary>
        /// завершено
        /// </summary>
        Ok,

        /// <summary>
        /// завершено с ошибками
        /// </summary>
        Error
    }

    /// <summary>
    /// Форма Асинхронного диалога прогресса 
    /// </summary>
    public partial class ProgressBgDlg : Form
    {
        #region Константы

        /// <summary>
        /// Минимальная ширина формы
        /// </summary>
        private const int EXTENDED_PANEL_WIDTH_MIN = 350;

        /// <summary>
        /// Минимальная высота формы
        /// </summary>
        private const int EXTENDED_PANEL_HEIGHT_MIN = 150;

        /// <summary>
        /// Высота размера Caption формы
        /// </summary>
        private const int CAPTION_HEIGHT = 24;

        #endregion

        /// <summary>
        /// Событие - нажата кнопка отмены
        /// </summary>
        public event EventHandler OnPushCancel;

        /// <summary>
        /// Предыдущий размер высоты при расширенном режиме
        /// </summary>
        private int _extendedHeight = EXTENDED_PANEL_HEIGHT_MIN;

        /// <summary>
        /// Заголовок диалога прогреса
        /// </summary>
        public string Title
        {
            get { return this.Text; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.SetTitle(@"В процессе...");
                else
                    this.SetTitle(value);
            }
        }

        /// <summary>
        /// Установить заголовок формы
        /// </summary>
        /// <param name="value">заголовок</param>
        private void SetTitle(string value)
        {
            //потоконезависимое присвоение
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(delegate { this.Text = value; }));
            else
                this.Text = value;
        }

        /// <summary>
        /// Описание прогреса
        /// </summary>
        public string Caption
        {
            get { return this.labelCaption.Text; }
            set
            {
                //потоконезависимое присвоение
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(delegate { this.labelCaption.Text = value; }));
                else
                    this.labelCaption.Text = value;
            }
        }

        /// <summary>
        /// Разрешение закрыть форму
        /// </summary>
        private bool TrustClose { get; set; }

        /// <summary>
        /// Возможность отмены процесса
        /// </summary>
        public bool Cancelled
        {
            get { return this.buttonCancel.Enabled; }
            set
            {
                //потоконезависимое присвоение
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(delegate { this.buttonCancel.Enabled = value; }));
                else
                    this.buttonCancel.Enabled = value;
            }
        }

        /// <summary>
        /// Возможность расширенного режима
        /// </summary>
        public bool Extended
        {
            get { return this.buttonExtended.Enabled; }
            set
            {
                //потоконезависимое присвоение
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(delegate { this.buttonExtended.Enabled = value; }));
                else
                    this.buttonExtended.Enabled = value;
            }
        }

        /// <summary>
        /// Закрывать окно при успешном завершении
        /// </summary>
        private bool CloseIfSuccessfulComplete
        {
            get { return this.checkBoxCloseComplette.Checked; }
            set
            {
                //потоконезависимое присвоение
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(delegate { this.checkBoxCloseComplette.Checked = value; }));
                else
                    this.checkBoxCloseComplette.Checked = value;
            }
        }

        /// <summary>
        /// Были ли ошибки
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Асинхронный вывод диалогового окна (или модально)
        /// </summary>
        public bool AsyncDialog { get; set; }

        /// <summary>
        /// Проценты в бегущем прогресс баре
        /// </summary>        
        public int PercentProgress
        {
            get { return this.prgBar.Value; }
            set
            {
                //потоконезависимое присвоение
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(delegate { this.prgBar.Value = value; }));
                else
                    this.prgBar.Value = value;
            }
        }


        /// <summary>
        /// Конструктор
        /// </summary>
        public ProgressBgDlg()
        {
            InitializeComponent();
            this.VisibleExtendedPanel(false);
            this.Title = @" Выполнение задания";
            this.Caption = @"Выполнение задания";
            this.TrustClose = false;
            this.Cancelled = true;
            this.Extended = true;
            this.CloseIfSuccessfulComplete = true;
            this.IsError = false;
            this.AsyncDialog = true;
            this.PercentProgress = 0;
        }

        /// <summary>
        /// Нажатие клавиши отмена
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (this.OnPushCancel != null)
                this.OnPushCancel(this, e);
            else
                this.CloseAfterWork(true);
        }

        /// <summary>
        /// Нажатие клавиши расширенный режим
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExtended_Click(object sender, EventArgs e)
        {
            this.ChangeVisibleExtendedPanel();
        }

        /// <summary>
        /// Сменить видимость панели с дополнениями
        /// </summary>
        private void ChangeVisibleExtendedPanel()
        {
            this.VisibleExtendedPanel(!this.panelExtended.Visible);
        }

        /// <summary>
        /// Убрать или показать дополнительную информацию
        /// </summary>
        /// <param name="visible">видимость</param>
        public void VisibleExtendedPanel(bool visible)
        {
            this.panelExtended.Visible = visible;
            if (this.panelExtended.Visible)
            {
                this.MinimumSize = new Size(EXTENDED_PANEL_WIDTH_MIN,
                    EXTENDED_PANEL_HEIGHT_MIN + (this.FormBorderStyle == FormBorderStyle.None ? 0 : CAPTION_HEIGHT) + this.panelCaption.Height + this.panelProgress.Height);
                this.MaximumSize = new Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width,
                    System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height);
                this.buttonExtended.Image = Properties.Resources.ie_up;
                this.Size = new Size(this.Size.Width, this._extendedHeight);
            }
            else
            {
                this._extendedHeight = this.Size.Height;
                this.MinimumSize = new Size(EXTENDED_PANEL_WIDTH_MIN,
                    (this.FormBorderStyle == FormBorderStyle.None ? 0 : CAPTION_HEIGHT) + this.panelCaption.Height + this.panelProgress.Height);
                this.MaximumSize = new Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width,
                    (this.FormBorderStyle == FormBorderStyle.None ? 0 : CAPTION_HEIGHT) + this.panelCaption.Height + this.panelProgress.Height);
                this.buttonExtended.Image = Properties.Resources.ie_down;
            }
        }

        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="forceClose">true - принудительное закрытие</param>
        public void CloseAfterWork(bool forceClose)
        {
            if (forceClose || !this.Extended) //если принудительное закрытие или нет расширенного режима то закрыть окно
            {
                this.TrustClose = true;
                this.Close();
            }
            else
            {
                if (this.CloseIfSuccessfulComplete && !this.IsError) //если стоит гаолчка закрытия и ошибок нет то закрыть 
                {
                    this.TrustClose = true;
                    this.Close();
                }
                else
                {
                    this.Cancelled = true;
                    if (this.IsError)
                        this.VisibleExtendedPanel(true);
                }
            }
        }

        /// <summary>
        /// Вывод коментариев
        /// </summary>
        /// <param name="s">строка с коментарием</param>
        public void WriteComment(string s)
        {
            //Если в разных потоках то через Invoke
            if (this.textBoxComment.InvokeRequired)
                //this.textBoxComment.Invoke(new WriteCommentDelegate(this.textBoxComment.AppendText), new object[] { s + Environment.NewLine });
                //BeginInvoke((MethodInvoker)(() => { SetTextboxText(s); }));
                this.textBoxComment.Invoke(new MethodInvoker(delegate { this.textBoxComment.AppendText(s + Environment.NewLine); }));
            else
                this.textBoxComment.AppendText(s + Environment.NewLine);
        }

        /// <summary>
        /// сменить иконку
        /// </summary>
        /// <param name="icon">тип иконки</param>
        public void ChangeIcon(ProgressBgState icon)
        {
            switch (icon)
            {
                case ProgressBgState.Progress:
                    this.pictureBox.Image = Properties.Resources.icon_rol;
                    break;
                case ProgressBgState.Cancel:
                    this.pictureBox.Image = Properties.Resources.icon_que;
                    break;
                case ProgressBgState.Ok:
                    this.pictureBox.Image = Properties.Resources.icon_sad;
                    break;
                case ProgressBgState.Error:
                    this.pictureBox.Image = Properties.Resources.icon_smi;
                    break;
            }
        }

        /// <summary>
        /// Попытка закрыть программу
        /// </summary>
        private void ProgressBgDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            //при закрытии формы возможны ошибки в закрчтии потока
            if (!this.TrustClose && this.OnPushCancel != null)
            {
                this.OnPushCancel(this, e);
                e.Cancel = true;
            }
            else
                e.Cancel = false;
        }

        /// <summary>
        /// Перехват нажатий клавишь
        /// </summary>
        private void ProgressBgDlg_KeyDown(object sender, KeyEventArgs e)
        {
            //перехват Alt + F4
            if (e.Alt && e.KeyCode == Keys.F4)
                e.Handled = true;
        }

        /// <summary>
        /// Переопределяем метод потери фокуса для модальности
        /// </summary>        
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            //если вывод не асинхронный то запрещаем потерю фокуса
            if (!this.AsyncDialog)
                this.Focus();
        }

    }

}
