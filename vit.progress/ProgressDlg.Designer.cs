namespace vit.progress
{
    partial class ProgressDlg
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelCaption = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.textBoxComment = new System.Windows.Forms.RichTextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.labelCaption);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 55);
            this.panel1.TabIndex = 1;
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Location = new System.Drawing.Point(68, 21);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(125, 13);
            this.labelCaption.TabIndex = 1;
            this.labelCaption.Text = "Подождите немного ....";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::vit.progress.Properties.Resources.icon_rol;
            this.pictureBox1.Location = new System.Drawing.Point(7, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(12, 65);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(335, 23);
            this.prgBar.TabIndex = 2;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComment.Location = new System.Drawing.Point(12, 98);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.ReadOnly = true;
            this.textBoxComment.Size = new System.Drawing.Size(416, 154);
            this.textBoxComment.TabIndex = 3;
            this.textBoxComment.Text = "";
            this.textBoxComment.WordWrap = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(353, 65);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Закрыть";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ProgressDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 264);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.prgBar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(350, 135);
            this.Name = "ProgressDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "В процессе ...";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.RichTextBox textBoxComment;
        private System.Windows.Forms.Button buttonCancel;
    }
}