using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mmr.plugins;

namespace mmr.Prognoz
{
    public partial class TestOptions : SimpleOptionsMDIForm, IOptionsMDIForm
    {
        public TestOptions()
        {
            InitializeComponent();
            textBox1.TextChanged += new EventHandler(base.Parameters_Changed);
        }
        
        #region реализация IOptionsMDIForm

        string IPluginsToMenu.ParentMenu { get { return @"Менюшко"; } }

        string IPluginsToMenu.NameForm { get { return @"Тестовая форма"; } }

        Icon IPluginsToMenu.Icon { get { return Properties.Resources.Crash_Catcher; } }

        void IOptionsMDIForm.LoadData()
        {            
            this.textBox1.Text = "загружено";            
        }

        void IOptionsMDIForm.SaveData()
        {
            this.textBox1.Text = "сохранено";            
        }

        #endregion
    }
}
