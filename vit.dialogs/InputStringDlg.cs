using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace vit.dialogs
{
    /// <summary>
    /// Форма ввода строки 
    /// </summary>
    public partial class InputStringDlg : Form
    {
        /// <summary>
        /// Вызов формы для ввода строки
        /// </summary>
        /// <param name="caption">Заголовок - описание вводимых данных</param>
        /// <param name="newParam">Введеное значение</param>
        /// <returns>были введены данные?</returns>
        public static bool GetString(string caption, ref string newParam)
        {
            InputStringDlg dlg = new InputStringDlg();
            dlg.labelCaption.Text = string.IsNullOrEmpty(caption) ? "Введите данные" : caption;
            dlg.textBoxData.Text = string.IsNullOrEmpty(newParam) ? string.Empty : newParam;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                newParam = dlg.textBoxData.Text;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public InputStringDlg()
        {
            InitializeComponent();
        }

    }
}
