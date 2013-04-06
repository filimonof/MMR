namespace vit.progress
{
    partial class ProgressBgDlg
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
            this.panelCaption = new System.Windows.Forms.Panel();
            this.labelCaption = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.buttonExtended = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.panelExtended = new System.Windows.Forms.Panel();
            this.checkBoxCloseComplette = new System.Windows.Forms.CheckBox();
            this.textBoxComment = new System.Windows.Forms.RichTextBox();
            this.panelCaption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panelProgress.SuspendLayout();
            this.panelExtended.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCaption
            // 
            this.panelCaption.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelCaption.Controls.Add(this.labelCaption);
            this.panelCaption.Controls.Add(this.pictureBox);
            this.panelCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCaption.Location = new System.Drawing.Point(0, 0);
            this.panelCaption.Name = "panelCaption";
            this.panelCaption.Size = new System.Drawing.Size(370, 55);
            this.panelCaption.TabIndex = 2;
            // 
            // labelCaption
            // 
            this.labelCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCaption.Location = new System.Drawing.Point(59, 9);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(311, 37);
            this.labelCaption.TabIndex = 0;
            this.labelCaption.Text = "Подождите немного ....";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox
            // 
            this.pictureBox.ErrorImage = null;
            this.pictureBox.Image = global::vit.progress.Properties.Resources.icon_rol;
            this.pictureBox.Location = new System.Drawing.Point(7, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(50, 50);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.buttonExtended);
            this.panelProgress.Controls.Add(this.buttonCancel);
            this.panelProgress.Controls.Add(this.prgBar);
            this.panelProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProgress.Location = new System.Drawing.Point(0, 55);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(370, 65);
            this.panelProgress.TabIndex = 3;
            // 
            // buttonExtended
            // 
            this.buttonExtended.FlatAppearance.BorderSize = 0;
            this.buttonExtended.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExtended.Image = global::vit.progress.Properties.Resources.ie_down;
            this.buttonExtended.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExtended.Location = new System.Drawing.Point(12, 36);
            this.buttonExtended.Name = "buttonExtended";
            this.buttonExtended.Size = new System.Drawing.Size(94, 21);
            this.buttonExtended.TabIndex = 2;
            this.buttonExtended.TabStop = false;
            this.buttonExtended.Text = "Подробнее ";
            this.buttonExtended.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExtended.UseVisualStyleBackColor = true;
            this.buttonExtended.Click += new System.EventHandler(this.buttonExtended_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Image = global::vit.progress.Properties.Resources.cross2;
            this.buttonCancel.Location = new System.Drawing.Point(331, 11);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(27, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(12, 11);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(313, 23);
            this.prgBar.TabIndex = 2;
            // 
            // panelExtended
            // 
            this.panelExtended.Controls.Add(this.checkBoxCloseComplette);
            this.panelExtended.Controls.Add(this.textBoxComment);
            this.panelExtended.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExtended.Location = new System.Drawing.Point(0, 120);
            this.panelExtended.Name = "panelExtended";
            this.panelExtended.Padding = new System.Windows.Forms.Padding(5);
            this.panelExtended.Size = new System.Drawing.Size(370, 161);
            this.panelExtended.TabIndex = 4;
            // 
            // checkBoxCloseComplette
            // 
            this.checkBoxCloseComplette.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCloseComplette.AutoSize = true;
            this.checkBoxCloseComplette.Location = new System.Drawing.Point(12, 140);
            this.checkBoxCloseComplette.Name = "checkBoxCloseComplette";
            this.checkBoxCloseComplette.Size = new System.Drawing.Size(221, 17);
            this.checkBoxCloseComplette.TabIndex = 3;
            this.checkBoxCloseComplette.TabStop = false;
            this.checkBoxCloseComplette.Text = "Закрыть, если выполнено без ошибок";
            this.checkBoxCloseComplette.UseVisualStyleBackColor = true;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComment.Location = new System.Drawing.Point(2, 4);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.ReadOnly = true;
            this.textBoxComment.Size = new System.Drawing.Size(365, 131);
            this.textBoxComment.TabIndex = 4;
            this.textBoxComment.TabStop = false;
            this.textBoxComment.Text = "";
            this.textBoxComment.WordWrap = false;
            // 
            // ProgressBgDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 281);
            this.ControlBox = false;
            this.Controls.Add(this.panelExtended);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.panelCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "ProgressBgDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "В процессе ...";
            this.TopMost = true;            
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressBgDlg_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProgressBgDlg_KeyDown);
            this.panelCaption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelExtended.ResumeLayout(false);
            this.panelExtended.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCaption;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Panel panelExtended;
        private System.Windows.Forms.RichTextBox textBoxComment;
        private System.Windows.Forms.Button buttonExtended;
        private System.Windows.Forms.CheckBox checkBoxCloseComplette;
    }
}