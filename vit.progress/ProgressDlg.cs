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
    /// Форма синхронного прогресса 
    /// </summary>
    public partial class ProgressDlg : Form
    {
        /// <summary>
        /// были ошибки во время работы
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// текущее положение прогресса
        /// </summary>
        private int _currentNext;
        
        /// <summary>
        /// количество шагов в прогрессе
        /// </summary>
        public int CountNext { get; set; }

        /// <summary>
        /// заголовок диалога прогреса
        /// </summary>
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        /// <summary>
        /// описание прогреса
        /// </summary>
        public string Caption
        {
            get { return this.labelCaption.Text; }
            set { this.labelCaption.Text = value; }
        }

        /// <summary>
        /// возможность отмены процесса
        /// </summary>
        private bool _cancelled;

        /// <summary>
        /// возможность отмены процесса
        /// </summary>
        public bool Cancelled
        {
            get { return this._cancelled; }
            set
            {
                this._cancelled = value;
                this.prgBar.Width = this._cancelled ? 335 : 416;
                this.buttonCancel.Visible = this._cancelled;
            }
        }

        /// <summary>
        /// конструктор прогресса
        /// </summary>
        /// <param name="caption">описание</param>
        /// <param name="cancelled">кнопка отмены</param>
        public ProgressDlg(string caption, bool cancelled)
        {
            InitializeComponent();
            this.Caption = caption;
            this.Cancelled = cancelled;
            this.IsError = false;
        }

        /// <summary>
        /// рост прогреса
        /// </summary>
        public void Next()
        {
            this._currentNext += 1;
            if ((this.CountNext > 0) && (this._currentNext / this.CountNext <= 1))
                this.prgBar.Value = (int)(100 * this._currentNext / this.CountNext);
            Application.DoEvents();
        }

        /// <summary>
        /// добавление коментария
        /// </summary>
        /// <param name="text">текст коментария</param>
        public void WriteComment(string text)
        {
            WriteComment(text, true);
        }

        /// <summary>
        /// добавление коментария без перевод на новую строку
        /// </summary>
        /// <param name="text">текст коментария</param>
        /// <param name="newline">нужен переход на новую строку</param>
        public void WriteComment(string text, bool newline)
        {
            this.textBoxComment.Text += text + (newline ? Environment.NewLine : string.Empty);
            Application.DoEvents();
        }

        /// <summary>
        /// нажата кнопка отмены
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Заблокировать кнопку отмены
        /// </summary>
        public void ButtonDisabled()
        {
            this.buttonCancel.Enabled = false;
        }

        /// <summary>
        /// Разблокировать кнопку отмены
        /// </summary>
        public void ButtonEnabled()
        {
            this.buttonCancel.Enabled = true;
        }
    }
}
