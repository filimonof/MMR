namespace vit.progress
{
    partial class WaitingDlg
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
            this.pictureBoxWaiting = new System.Windows.Forms.PictureBox();
            this.labelLoad = new System.Windows.Forms.Label();
            this.panelWaiting = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWaiting)).BeginInit();
            this.panelWaiting.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxWaiting
            // 
            this.pictureBoxWaiting.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxWaiting.Image = global::vit.progress.Properties.Resources.icon_rol;
            this.pictureBoxWaiting.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxWaiting.Name = "pictureBoxWaiting";
            this.pictureBoxWaiting.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxWaiting.TabIndex = 0;
            this.pictureBoxWaiting.TabStop = false;
            // 
            // labelLoad
            // 
            this.labelLoad.Location = new System.Drawing.Point(68, 12);
            this.labelLoad.Name = "labelLoad";
            this.labelLoad.Size = new System.Drawing.Size(160, 50);
            this.labelLoad.TabIndex = 1;
            this.labelLoad.Text = "Загрузка данных ....";
            this.labelLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelWaiting
            // 
            this.panelWaiting.BackColor = System.Drawing.Color.White;
            this.panelWaiting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWaiting.Controls.Add(this.labelLoad);
            this.panelWaiting.Controls.Add(this.pictureBoxWaiting);
            this.panelWaiting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWaiting.Location = new System.Drawing.Point(0, 0);
            this.panelWaiting.Name = "panelWaiting";
            this.panelWaiting.Size = new System.Drawing.Size(241, 73);
            this.panelWaiting.TabIndex = 2;
            // 
            // WaitingDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 73);
            this.ControlBox = false;
            this.Controls.Add(this.panelWaiting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitingDlg";
            this.Opacity = 0.7D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ждите ...";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWaiting)).EndInit();
            this.panelWaiting.ResumeLayout(false);
            this.panelWaiting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxWaiting;
        private System.Windows.Forms.Label labelLoad;
        private System.Windows.Forms.Panel panelWaiting;
    }
}