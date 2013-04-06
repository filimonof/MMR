using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mmr.plugins;
using mmr.share;

namespace mmr
{
    public partial class CK7Options : SimpleOptionsMDIForm, IOptionsMDIForm
    {
        public CK7Options()
        {
            InitializeComponent();

            this.checkBoxCKEnabled.CheckedChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxCKName.TextChanged += new EventHandler(base.Parameters_Changed);  

            this.textBoxCK1_Name.TextChanged += new EventHandler(base.Parameters_Changed); 
            this.checkBoxCK1_WinNT.CheckedChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxCK1_Login.TextChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxCK1_Pas.TextChanged += new EventHandler(base.Parameters_Changed);

            this.textBoxCK2_Name.TextChanged += new EventHandler(base.Parameters_Changed); 
            this.checkBoxCK2_WinNT.CheckedChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxCK2_Login.TextChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxCK2_Pas.TextChanged += new EventHandler(base.Parameters_Changed);

            this.textBoxCK3_Name.TextChanged += new EventHandler(base.Parameters_Changed); 
            this.checkBoxCK3_WinNT.CheckedChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxCK3_Login.TextChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxCK3_Pas.TextChanged += new EventHandler(base.Parameters_Changed); 
        }

        #region реализация IOptionsMDIForm
        string IPluginsToMenu.NameForm { get { return @"Подключение к СК-2007"; } }

        string IPluginsToMenu.ParentMenu { get { return null; } }

        Icon IPluginsToMenu.Icon { get { return null; } }

        void IOptionsMDIForm.LoadData()
        {
            ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(Vars.CON_STR);
            param.Open();
            this.checkBoxCKEnabled.Checked = param.Get(ParameterShareEnum.CK_Enabled, false);
            this.textBoxCKName.Text = param.Get(ParameterShareEnum.CK_Name, string.Empty);

            this.textBoxCK1_Name.Text = param.Get(ParameterShareEnum.CK_Server1_Name, string.Empty);
            this.checkBoxCK1_WinNT.Checked = param.Get(ParameterShareEnum.CK_Server1_WinNT, false);
            this.textBoxCK1_Login.Text = param.Get(ParameterShareEnum.CK_Server1_Login, string.Empty);
            this.textBoxCK1_Pas.Text = param.Get(ParameterShareEnum.CK_Server1_Pas, string.Empty);

            this.textBoxCK2_Name.Text = param.Get(ParameterShareEnum.CK_Server2_Name, string.Empty);
            this.checkBoxCK2_WinNT.Checked = param.Get(ParameterShareEnum.CK_Server2_WinNT, false);
            this.textBoxCK2_Login.Text = param.Get(ParameterShareEnum.CK_Server2_Login, string.Empty);
            this.textBoxCK2_Pas.Text = param.Get(ParameterShareEnum.CK_Server2_Pas, string.Empty);

            this.textBoxCK3_Name.Text = param.Get(ParameterShareEnum.CK_Server3_Name, string.Empty);
            this.checkBoxCK3_WinNT.Checked = param.Get(ParameterShareEnum.CK_Server3_WinNT, false);
            this.textBoxCK3_Login.Text = param.Get(ParameterShareEnum.CK_Server3_Login, string.Empty);
            this.textBoxCK3_Pas.Text = param.Get(ParameterShareEnum.CK_Server3_Pas, string.Empty);           
        }

        void IOptionsMDIForm.SaveData()
        {
            ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(Vars.CON_STR);
            param.Set(ParameterShareEnum.CK_Enabled, this.checkBoxCKEnabled.Checked);
            param.Set(ParameterShareEnum.CK_Name, this.textBoxCKName.Text);

            param.Set(ParameterShareEnum.CK_Server1_Name, this.textBoxCK1_Name.Text);
            param.Set(ParameterShareEnum.CK_Server1_WinNT, this.checkBoxCK1_WinNT.Checked);
            param.Set(ParameterShareEnum.CK_Server1_Login, this.textBoxCK1_Login.Text);
            param.Set(ParameterShareEnum.CK_Server1_Pas, this.textBoxCK1_Pas.Text);

            param.Set(ParameterShareEnum.CK_Server2_Name, this.textBoxCK2_Name.Text);
            param.Set(ParameterShareEnum.CK_Server2_WinNT, this.checkBoxCK2_WinNT.Checked);
            param.Set(ParameterShareEnum.CK_Server2_Login, this.textBoxCK2_Login.Text);
            param.Set(ParameterShareEnum.CK_Server2_Pas, this.textBoxCK2_Pas.Text);

            param.Set(ParameterShareEnum.CK_Server3_Name, this.textBoxCK3_Name.Text);
            param.Set(ParameterShareEnum.CK_Server3_WinNT, this.checkBoxCK3_WinNT.Checked);
            param.Set(ParameterShareEnum.CK_Server3_Login, this.textBoxCK3_Login.Text);
            param.Set(ParameterShareEnum.CK_Server3_Pas, this.textBoxCK3_Pas.Text);
        }
        #endregion
    }
}
