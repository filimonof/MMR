using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mmr.plugins;
using mmr.share;
using vit.CK7;

namespace mmr.Prognoz
{
    public partial class PrognozOtkOptions : SimpleOptionsMDIForm, IOptionsMDIForm
    {
        /// <summary>статусы прогноза</summary>
        private Dictionary<int, string> _statusPrognoz;

        /// <summary>схемы прогноза</summary>
        private Dictionary<int, string> _shemaPrognoz;

        /// <summary>територии прогноза</summary>
        private Dictionary<int, string> _areaPrognoz;

        /// <summary>Конструктор</summary>
        public PrognozOtkOptions()
        {
            InitializeComponent();

            this.comboBoxShema.SelectedIndexChanged += new EventHandler(base.Parameters_Changed);
            this.comboBoxArea.SelectedIndexChanged += new EventHandler(base.Parameters_Changed);
            this.comboBoxStatus.SelectedIndexChanged += new EventHandler(base.Parameters_Changed);

            this.textBoxTI.TextChanged += new EventHandler(base.Parameters_Changed);
            this.comboBoxCat.SelectedIndexChanged += new EventHandler(base.Parameters_Changed);
            this.checkBoxAvg.CheckedChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxNameColumn.TextChanged += new EventHandler(base.Parameters_Changed);
            this.textBoxNameColumnOtkl.TextChanged += new EventHandler(base.Parameters_Changed);
        }

        #region реализация IOptionsMDIForm
        string IPluginsToMenu.NameForm { get { return @"Отклонение факта от прогноза потребления"; } }

        string IPluginsToMenu.ParentMenu { get { return null; } }

        Icon IPluginsToMenu.Icon { get { return null; } }

        void IOptionsMDIForm.LoadData()
        {
            ParameterClass<ParameterPrognozEnum> param = new ParameterClass<ParameterPrognozEnum>(Vars.CON_STR);
            param.Open();
            int val = 0;
            
            //схема прогноза
            this._shemaPrognoz = null;
            this._shemaPrognoz = PrognozUtils.Shemas(Vars.CON_STR_PROGNOZ);
            if (this._shemaPrognoz.Count > 0)
            {
                this.comboBoxShema.DataSource = new BindingSource(this._shemaPrognoz, null);
                this.comboBoxShema.DisplayMember = "Value";
                this.comboBoxShema.ValueMember = "Key";
                if (int.TryParse(param.Get(ParameterPrognozEnum.PrognozOtkl_PP_Shema, string.Empty), out val))
                    this.comboBoxShema.SelectedValue = val;
            }
            else this.comboBoxShema.DataSource = null;


            //территория прогноза
            this._areaPrognoz = null;
            this._areaPrognoz = PrognozUtils.Areas(Vars.CON_STR_PROGNOZ, val);
            if (this._areaPrognoz.Count > 0)
            {
                this.comboBoxArea.DataSource = new BindingSource(this._areaPrognoz, null);
                this.comboBoxArea.DisplayMember = "Value";
                this.comboBoxArea.ValueMember = "Key";
                if (int.TryParse(param.Get(ParameterPrognozEnum.PrognozOtkl_PP_Area, string.Empty), out val))
                    this.comboBoxArea.SelectedValue = val;
            }
            else this.comboBoxArea.DataSource = null;

            //статус прогноза
            this._statusPrognoz = null;
            this._statusPrognoz = PrognozUtils.StatPrognoz(Vars.CON_STR_PROGNOZ);
            if (this._statusPrognoz.Count > 0)
            {
                this.comboBoxStatus.DataSource = new BindingSource(this._statusPrognoz, null);
                this.comboBoxStatus.DisplayMember = "Value";
                this.comboBoxStatus.ValueMember = "Key";
                if (int.TryParse(param.Get(ParameterPrognozEnum.PrognozOtkl_PP_Status, string.Empty), out val))
                    this.comboBoxStatus.SelectedValue = val;
            }
            else this.comboBoxStatus.DataSource = null;

            this.textBoxTI.Text = param.Get(ParameterPrognozEnum.PrognozOtkl_CK_TI, string.Empty);
            this.comboBoxCat.DataSource = typeof(Category).ToList(true);
            this.comboBoxCat.DisplayMember = "Value";
            this.comboBoxCat.ValueMember = "Key";
            this.comboBoxCat.SelectedValue = param.Get(ParameterPrognozEnum.PrognozOtkl_CK_Cat, Category.TI);

            this.checkBoxAvg.Checked = param.Get(ParameterPrognozEnum.PrognozOtkl_Avg, false);
            this.textBoxNameColumn.Text = param.Get(ParameterPrognozEnum.PrognozOtkl_ColumnName, string.Empty);
            this.textBoxNameColumnOtkl.Text = param.Get(ParameterPrognozEnum.PrognozOtkl_ColumnOtklName, string.Empty);
        }

        void IOptionsMDIForm.SaveData()
        {
            ParameterClass<ParameterPrognozEnum> param = new ParameterClass<ParameterPrognozEnum>(Vars.CON_STR);
            if (this.comboBoxShema.SelectedValue != null)
                param.Set(ParameterPrognozEnum.PrognozOtkl_PP_Shema, (int)this.comboBoxShema.SelectedValue);
            else
                param.Set(ParameterPrognozEnum.PrognozOtkl_PP_Shema, string.Empty);

            if (this.comboBoxArea.SelectedValue != null)
                param.Set(ParameterPrognozEnum.PrognozOtkl_PP_Area, (int)this.comboBoxArea.SelectedValue);
            else
                param.Set(ParameterPrognozEnum.PrognozOtkl_PP_Area, string.Empty);

            if (this.comboBoxStatus.SelectedValue != null)
                param.Set(ParameterPrognozEnum.PrognozOtkl_PP_Status, (int)this.comboBoxStatus.SelectedValue);
            else
                param.Set(ParameterPrognozEnum.PrognozOtkl_PP_Status, string.Empty);

            param.Set(ParameterPrognozEnum.PrognozOtkl_CK_TI, this.textBoxTI.Text);
            param.Set(ParameterPrognozEnum.PrognozOtkl_CK_Cat, (Category)this.comboBoxCat.SelectedValue);

            param.Set(ParameterPrognozEnum.PrognozOtkl_Avg, this.checkBoxAvg.Checked);
            param.Set(ParameterPrognozEnum.PrognozOtkl_ColumnName, this.textBoxNameColumn.Text);
            param.Set(ParameterPrognozEnum.PrognozOtkl_ColumnOtklName, this.textBoxNameColumnOtkl.Text);
        }
        #endregion

        /// <summary>Изменение схемы</summary>
        private void comboBoxShema_SelectedIndexChanged(object sender, EventArgs e)
        {
            //территория прогноза
            int i;
            if (int.TryParse(this.comboBoxShema.SelectedValue.ToString(), out i))
            {
                this._areaPrognoz = null;
                this._areaPrognoz = PrognozUtils.Areas(Vars.CON_STR_PROGNOZ, i);
                if (this._areaPrognoz.Count > 0)
                {
                    this.comboBoxArea.DataSource = new BindingSource(this._areaPrognoz, null);
                    this.comboBoxArea.DisplayMember = "Value";
                    this.comboBoxArea.ValueMember = "Key";
                }
                else this.comboBoxArea.DataSource = null;
            }            
        }
    }
}
