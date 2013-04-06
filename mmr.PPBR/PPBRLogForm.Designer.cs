namespace mmr
{
    partial class PPBRLogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ComboBoxDate = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ComboBoxMonth = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ComboBoxYear = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.MenyDownLoadAndUpload = new System.Windows.Forms.ToolStripSplitButton();
            this.MenuAutoLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReplay = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMaketCK = new System.Windows.Forms.ToolStripMenuItem();
            this.ButtonShowData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonClose = new System.Windows.Forms.ToolStripButton();
            this.tblPPBRLogsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetPPBR = new mmr.DataSetPPBR();
            this.tblPPBR_LogsTableAdapter = new mmr.DataSetPPBRTableAdapters.tblPPBR_LogsTableAdapter();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateEventDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateMaketDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridLogsPPBR = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblPPBRLogsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPPBR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLogsPPBR)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ComboBoxDate,
            this.toolStripLabel1,
            this.ComboBoxMonth,
            this.toolStripLabel2,
            this.ComboBoxYear,
            this.toolStripSeparator1,
            this.ButtonRefresh,
            this.MenyDownLoadAndUpload,
            this.ButtonShowData,
            this.toolStripSeparator3,
            this.ButtonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(845, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ComboBoxDate
            // 
            this.ComboBoxDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDate.MaxDropDownItems = 2;
            this.ComboBoxDate.Name = "ComboBoxDate";
            this.ComboBoxDate.Size = new System.Drawing.Size(75, 25);
            this.ComboBoxDate.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDate_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(82, 22);
            this.toolStripLabel1.Text = " ППБР за месяц";
            // 
            // ComboBoxMonth
            // 
            this.ComboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMonth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "Сентябрь"});
            this.ComboBoxMonth.MaxDropDownItems = 12;
            this.ComboBoxMonth.Name = "ComboBoxMonth";
            this.ComboBoxMonth.Size = new System.Drawing.Size(80, 25);
            this.ComboBoxMonth.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMonth_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(31, 22);
            this.toolStripLabel2.Text = "  год";
            // 
            // ComboBoxYear
            // 
            this.ComboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxYear.DropDownWidth = 75;
            this.ComboBoxYear.Name = "ComboBoxYear";
            this.ComboBoxYear.Size = new System.Drawing.Size(75, 25);
            this.ComboBoxYear.SelectedIndexChanged += new System.EventHandler(this.ComboBoxYear_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Image = global::mmr.Properties.Resources.Refresh;
            this.ButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(77, 22);
            this.ButtonRefresh.Text = "Обновить";
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // MenyDownLoadAndUpload
            // 
            this.MenyDownLoadAndUpload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAutoLoad,
            this.MenuLoad,
            this.MenuReplay,
            this.MenuMaketCK});
            this.MenyDownLoadAndUpload.Image = global::mmr.Properties.Resources.SendPost;
            this.MenyDownLoadAndUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenyDownLoadAndUpload.Name = "MenyDownLoadAndUpload";
            this.MenyDownLoadAndUpload.Size = new System.Drawing.Size(85, 22);
            this.MenyDownLoadAndUpload.Text = "Загрузка";
            this.MenyDownLoadAndUpload.ButtonClick += new System.EventHandler(this.MenyDownLoadAndUpload_ButtonClick);
            // 
            // MenuAutoLoad
            // 
            this.MenuAutoLoad.Name = "MenuAutoLoad";
            this.MenuAutoLoad.Size = new System.Drawing.Size(240, 22);
            this.MenuAutoLoad.Text = "Загрузить из почты";
            this.MenuAutoLoad.Click += new System.EventHandler(this.MenuAutoLoad_Click);
            // 
            // MenuLoad
            // 
            this.MenuLoad.Image = global::mmr.Properties.Resources.folder;
            this.MenuLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuLoad.Name = "MenuLoad";
            this.MenuLoad.Size = new System.Drawing.Size(240, 22);
            this.MenuLoad.Text = "Загрузить с жесткого диска";
            this.MenuLoad.Click += new System.EventHandler(this.MenuLoad_Click);
            // 
            // MenuReplay
            // 
            this.MenuReplay.Name = "MenuReplay";
            this.MenuReplay.Size = new System.Drawing.Size(240, 22);
            this.MenuReplay.Text = "Пересылка макета  адресатам";
            this.MenuReplay.Click += new System.EventHandler(this.MenuReplay_Click);
            // 
            // MenuMaketCK
            // 
            this.MenuMaketCK.Name = "MenuMaketCK";
            this.MenuMaketCK.Size = new System.Drawing.Size(240, 22);
            this.MenuMaketCK.Text = "Создание макета для СК-2003";
            this.MenuMaketCK.Click += new System.EventHandler(this.MenuMaketCK_Click);
            // 
            // ButtonShowData
            // 
            this.ButtonShowData.Enabled = false;
            this.ButtonShowData.Image = global::mmr.Properties.Resources.prewiev;
            this.ButtonShowData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonShowData.Name = "ButtonShowData";
            this.ButtonShowData.Size = new System.Drawing.Size(75, 22);
            this.ButtonShowData.Text = "Просмотр";
            this.ButtonShowData.Click += new System.EventHandler(this.ButtonShowData_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Image = global::mmr.Properties.Resources.Cancel;
            this.ButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(71, 22);
            this.ButtonClose.Text = "Закрыть";
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click_1);
            // 
            // tblPPBRLogsBindingSource
            // 
            this.tblPPBRLogsBindingSource.DataMember = "tblPPBR_Logs";
            this.tblPPBRLogsBindingSource.DataSource = this.dataSetPPBR;
            // 
            // dataSetPPBR
            // 
            this.dataSetPPBR.DataSetName = "DataSetPPBR";
            this.dataSetPPBR.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblPPBR_LogsTableAdapter
            // 
            this.tblPPBR_LogsTableAdapter.ClearBeforeFill = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.FillWeight = 151.6811F;
            this.Status.HeaderText = "Статус";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // dateEventDataGridViewTextBoxColumn
            // 
            this.dateEventDataGridViewTextBoxColumn.DataPropertyName = "DateEvent";
            this.dateEventDataGridViewTextBoxColumn.FillWeight = 49.44918F;
            this.dateEventDataGridViewTextBoxColumn.HeaderText = "Дата события";
            this.dateEventDataGridViewTextBoxColumn.MinimumWidth = 15;
            this.dateEventDataGridViewTextBoxColumn.Name = "dateEventDataGridViewTextBoxColumn";
            this.dateEventDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateMaketDataGridViewTextBoxColumn
            // 
            this.dateMaketDataGridViewTextBoxColumn.DataPropertyName = "DateMaket";
            this.dateMaketDataGridViewTextBoxColumn.FillWeight = 49.66281F;
            this.dateMaketDataGridViewTextBoxColumn.HeaderText = "Дата макета";
            this.dateMaketDataGridViewTextBoxColumn.MinimumWidth = 15;
            this.dateMaketDataGridViewTextBoxColumn.Name = "dateMaketDataGridViewTextBoxColumn";
            this.dateMaketDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.FillWeight = 27.53512F;
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.MinimumWidth = 15;
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gridLogsPPBR
            // 
            this.gridLogsPPBR.AllowUserToAddRows = false;
            this.gridLogsPPBR.AllowUserToDeleteRows = false;
            this.gridLogsPPBR.AutoGenerateColumns = false;
            this.gridLogsPPBR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLogsPPBR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLogsPPBR.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.dateMaketDataGridViewTextBoxColumn,
            this.dateEventDataGridViewTextBoxColumn,
            this.Status});
            this.gridLogsPPBR.DataSource = this.tblPPBRLogsBindingSource;
            this.gridLogsPPBR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLogsPPBR.Location = new System.Drawing.Point(0, 25);
            this.gridLogsPPBR.Name = "gridLogsPPBR";
            this.gridLogsPPBR.ReadOnly = true;
            this.gridLogsPPBR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLogsPPBR.Size = new System.Drawing.Size(845, 373);
            this.gridLogsPPBR.TabIndex = 1;
            // 
            // PPBRLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(845, 398);
            this.Controls.Add(this.gridLogsPPBR);
            this.Controls.Add(this.toolStrip1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PPBRLogForm";
            this.Text = "Журнал событий ППБР";
            this.Load += new System.EventHandler(this.PPBRLogForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblPPBRLogsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPPBR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLogsPPBR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ButtonRefresh;
        private DataSetPPBR dataSetPPBR;
        private System.Windows.Forms.BindingSource tblPPBRLogsBindingSource;
        private mmr.DataSetPPBRTableAdapters.tblPPBR_LogsTableAdapter tblPPBR_LogsTableAdapter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox ComboBoxMonth;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox ComboBoxYear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ButtonShowData;
        private System.Windows.Forms.ToolStripButton ButtonClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateEventDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateMaketDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView gridLogsPPBR;
        private System.Windows.Forms.ToolStripComboBox ComboBoxDate;
        private System.Windows.Forms.ToolStripSplitButton MenyDownLoadAndUpload;
        private System.Windows.Forms.ToolStripMenuItem MenuAutoLoad;
        private System.Windows.Forms.ToolStripMenuItem MenuLoad;
        private System.Windows.Forms.ToolStripMenuItem MenuReplay;
        private System.Windows.Forms.ToolStripMenuItem MenuMaketCK;
    }
}
