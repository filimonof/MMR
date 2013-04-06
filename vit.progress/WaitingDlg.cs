using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace vit.progress
{
    /// <summary>
    /// Диалог ожидания при выполнении длительного действия
    /// </summary>
    public partial class WaitingDlg : Form
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public WaitingDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Признак активности диалога ожидания 
        /// </summary>
        private static bool enabled = false;

        /// <summary>
        /// Открыть диалог ожидания
        /// </summary>
        public static void ShowWaitingDlg()
        {
            WaitingDlg.ShowWaitingDlg(string.Empty);
        }

        /// <summary>
        /// Открыть диалог ожидания
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public static void ShowWaitingDlg(string text)
        {
            if (!WaitingDlg.enabled)
            {
                WaitingDlg.enabled = true;
                ThreadPool.QueueUserWorkItem(
                    (x) =>
                    {
                        using (WaitingDlg dialog = new WaitingDlg())
                        {
                            if (!string.IsNullOrEmpty(text))
                                dialog.labelLoad.Text = text;
                            dialog.Show();
                            dialog.BringToFront();
                            while (WaitingDlg.enabled)
                                Application.DoEvents();
                            dialog.Close();
                        }
                    });
            }
        }

        /// <summary>
        /// Закрыть диалог ожидания
        /// </summary>
        public static void CloseWaitingDlg()
        {
            WaitingDlg.enabled = false;
        }

    }
}
