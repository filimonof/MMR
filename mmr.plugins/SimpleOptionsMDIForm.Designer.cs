namespace mmr.plugins
{
    partial class SimpleOptionsMDIForm
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
            this.toolStripButtons = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
            this.panelCaption = new System.Windows.Forms.Panel();
            this.labelCaption = new System.Windows.Forms.Label();
            this.toolStripButtons.SuspendLayout();
            this.panelCaption.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripButtons
            // 
            this.toolStripButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSave,
            this.toolStripButtonCancel,
            this.toolStripSeparator1,
            this.toolStripButtonClose});
            this.toolStripButtons.Location = new System.Drawing.Point(0, 0);
            this.toolStripButtons.Name = "toolStripButtons";
            this.toolStripButtons.Size = new System.Drawing.Size(526, 25);
            this.toolStripButtons.TabIndex = 0;
            this.toolStripButtons.Text = "меню";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.Image = global::mmr.plugins.Properties.Resources.save;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(138, 22);
            this.toolStripButtonSave.Text = "Сохранить изменения";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.Image = global::mmr.plugins.Properties.Resources.Refresh;
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.Size = new System.Drawing.Size(133, 22);
            this.toolStripButtonCancel.Text = "Отменить изменения";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonClose
            // 
            this.toolStripButtonClose.Image = global::mmr.plugins.Properties.Resources.Cancel;
            this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClose.Name = "toolStripButtonClose";
            this.toolStripButtonClose.Size = new System.Drawing.Size(71, 22);
            this.toolStripButtonClose.Text = "Закрыть";
            this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
            // 
            // panelCaption
            // 
            this.panelCaption.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelCaption.Controls.Add(this.labelCaption);
            this.panelCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCaption.Location = new System.Drawing.Point(0, 25);
            this.panelCaption.Name = "panelCaption";
            this.panelCaption.Size = new System.Drawing.Size(526, 26);
            this.panelCaption.TabIndex = 1;
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCaption.Location = new System.Drawing.Point(12, 6);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(230, 13);
            this.labelCaption.TabIndex = 0;
            this.labelCaption.Text = "Форма-родитель для настроек программы ";
            // 
            // SimpleOptionsMDIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(526, 392);
            this.Controls.Add(this.panelCaption);
            this.Controls.Add(this.toolStripButtons);
            this.Name = "SimpleOptionsMDIForm";
            this.Text = "Simple Options MDI Form";
            this.Load += new System.EventHandler(this.SimpleOptionsMDIForm_Load);
            this.toolStripButtons.ResumeLayout(false);
            this.toolStripButtons.PerformLayout();
            this.panelCaption.ResumeLayout(false);
            this.panelCaption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripButtons;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonClose;
        private System.Windows.Forms.Panel panelCaption;
        protected System.Windows.Forms.Label labelCaption;
    }
}
