namespace mmr.PlanPotr
{
    partial class PlanPotrForm
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
            this.ButtonVisibleFactTable = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.ButtonLoad = new System.Windows.Forms.ToolStripButton();
            this.ButtonFactPotr = new System.Windows.Forms.ToolStripButton();
            this.ButtonGraph = new System.Windows.Forms.ToolStripButton();
            this.ButtonExportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonClose = new System.Windows.Forms.ToolStripButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewFact = new System.Windows.Forms.DataGridView();
            this.ButtonVisibleFactTable.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFact)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonVisibleFactTable
            // 
            this.ButtonVisibleFactTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripSeparator1,
            this.ButtonRefresh,
            this.ButtonLoad,
            this.ButtonFactPotr,
            this.ButtonGraph,
            this.ButtonExportExcel,
            this.toolStripSeparator3,
            this.ButtonClose});
            this.ButtonVisibleFactTable.Location = new System.Drawing.Point(0, 0);
            this.ButtonVisibleFactTable.Name = "ButtonVisibleFactTable";
            this.ButtonVisibleFactTable.Size = new System.Drawing.Size(692, 25);
            this.ButtonVisibleFactTable.TabIndex = 2;
            this.ButtonVisibleFactTable.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(219, 22);
            this.toolStripLabel2.Text = " Прогноз на                                                   ";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonRefresh
            // 
            //this.ButtonRefresh.Image = global::mmr.Properties.Resources.Refresh;
            this.ButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(77, 22);
            this.ButtonRefresh.Text = "Обновить";
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // ButtonLoad
            // 
            //this.ButtonLoad.Image = global::mmr.Properties.Resources.SendPost;
            this.ButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(79, 22);
            this.ButtonLoad.Text = "Загрузить";
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // ButtonFactPotr
            // 
            this.ButtonFactPotr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.ButtonFactPotr.Image = global::mmr.Properties.Resources.prewiev;
            this.ButtonFactPotr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFactPotr.Name = "ButtonFactPotr";
            this.ButtonFactPotr.Size = new System.Drawing.Size(23, 22);
            this.ButtonFactPotr.Text = "Факт";
            this.ButtonFactPotr.ToolTipText = "Скрыть/показать факт потребления";
            this.ButtonFactPotr.Click += new System.EventHandler(this.ButtonFactPotr_Click);
            // 
            // ButtonGraph
            // 
            this.ButtonGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.ButtonGraph.Image = global::mmr.Properties.Resources.prewiev;
            this.ButtonGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonGraph.Name = "ButtonGraph";
            this.ButtonGraph.Size = new System.Drawing.Size(23, 22);
            this.ButtonGraph.Text = "Графики";
            this.ButtonGraph.ToolTipText = "Скрыть/показать графики";
            // 
            // ButtonExportExcel
            // 
            this.ButtonExportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.ButtonExportExcel.Image = global::mmr.Properties.Resources.excel1;
            this.ButtonExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonExportExcel.Name = "ButtonExportExcel";
            this.ButtonExportExcel.Size = new System.Drawing.Size(23, 22);
            this.ButtonExportExcel.Text = "Экспорт";
            this.ButtonExportExcel.ToolTipText = "Экспорт в Excel";
            this.ButtonExportExcel.Click += new System.EventHandler(this.ButtonExportExcel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonClose
            // 
            //this.ButtonClose.Image = global::mmr.Properties.Resources.Cancel;
            this.ButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(71, 22);
            this.ButtonClose.Text = "Закрыть";
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(78, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(145, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewFact);
            this.splitContainer1.Panel2MinSize = 50;
            this.splitContainer1.Size = new System.Drawing.Size(692, 405);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(692, 262);
            this.dataGridView1.TabIndex = 5;
            // 
            // dataGridViewFact
            // 
            this.dataGridViewFact.AllowUserToAddRows = false;
            this.dataGridViewFact.AllowUserToDeleteRows = false;
            this.dataGridViewFact.AllowUserToOrderColumns = true;
            this.dataGridViewFact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFact.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFact.Name = "dataGridViewFact";
            this.dataGridViewFact.ReadOnly = true;
            this.dataGridViewFact.Size = new System.Drawing.Size(692, 138);
            this.dataGridViewFact.TabIndex = 6;
            // 
            // PlanPotrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(692, 430);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.ButtonVisibleFactTable);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PlanPotrForm";
            this.Text = "Планирование потребления";
            this.ButtonVisibleFactTable.ResumeLayout(false);
            this.ButtonVisibleFactTable.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFact)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ButtonVisibleFactTable;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton ButtonClose;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ToolStripButton ButtonLoad;
        private System.Windows.Forms.ToolStripButton ButtonFactPotr;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridViewFact;
        private System.Windows.Forms.ToolStripButton ButtonGraph;
        private System.Windows.Forms.ToolStripButton ButtonExportExcel;
    }
}
