namespace mmr.Prognoz
{
    partial class PrognozOtkOptions
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCat = new System.Windows.Forms.ComboBox();
            this.checkBoxAvg = new System.Windows.Forms.CheckBox();
            this.textBoxNameColumnOtkl = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxNameColumn = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTI = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxArea = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxShema = new System.Windows.Forms.ComboBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCaption
            // 
            this.labelCaption.Size = new System.Drawing.Size(434, 13);
            this.labelCaption.Text = "Настройки отчёта отклонение фактического потребления от прогноза потребления";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxCat);
            this.groupBox1.Controls.Add(this.checkBoxAvg);
            this.groupBox1.Controls.Add(this.textBoxNameColumnOtkl);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBoxNameColumn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxTI);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 149);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные СК";
            // 
            // comboBoxCat
            // 
            this.comboBoxCat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCat.FormattingEnabled = true;
            this.comboBoxCat.Location = new System.Drawing.Point(98, 41);
            this.comboBoxCat.Name = "comboBoxCat";
            this.comboBoxCat.Size = new System.Drawing.Size(422, 21);
            this.comboBoxCat.TabIndex = 2;
            // 
            // checkBoxAvg
            // 
            this.checkBoxAvg.AutoSize = true;
            this.checkBoxAvg.Location = new System.Drawing.Point(21, 68);
            this.checkBoxAvg.Name = "checkBoxAvg";
            this.checkBoxAvg.Size = new System.Drawing.Size(260, 17);
            this.checkBoxAvg.TabIndex = 3;
            this.checkBoxAvg.Text = "Усреднить значение ( (Час-1).30 + Час.00 ) / 2";
            this.checkBoxAvg.UseVisualStyleBackColor = true;
            // 
            // textBoxNameColumnOtkl
            // 
            this.textBoxNameColumnOtkl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNameColumnOtkl.Location = new System.Drawing.Point(210, 117);
            this.textBoxNameColumnOtkl.Name = "textBoxNameColumnOtkl";
            this.textBoxNameColumnOtkl.Size = new System.Drawing.Size(310, 20);
            this.textBoxNameColumnOtkl.TabIndex = 5;
            this.textBoxNameColumnOtkl.Tag = "";
            this.textBoxNameColumnOtkl.Text = "Отклонение от усредненного";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 120);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(186, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "Название столбца с отклонениями";
            // 
            // textBoxNameColumn
            // 
            this.textBoxNameColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNameColumn.Location = new System.Drawing.Point(119, 91);
            this.textBoxNameColumn.Name = "textBoxNameColumn";
            this.textBoxNameColumn.Size = new System.Drawing.Size(401, 20);
            this.textBoxNameColumn.TabIndex = 4;
            this.textBoxNameColumn.Tag = "";
            this.textBoxNameColumn.Text = "Усредненное потребление";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Название стлбца";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Категория";
            // 
            // textBoxTI
            // 
            this.textBoxTI.Location = new System.Drawing.Point(98, 16);
            this.textBoxTI.Name = "textBoxTI";
            this.textBoxTI.Size = new System.Drawing.Size(90, 20);
            this.textBoxTI.TabIndex = 1;
            this.textBoxTI.Tag = "";
            this.textBoxTI.Text = "4535";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "ТИ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxArea);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxShema);
            this.groupBox2.Controls.Add(this.comboBoxStatus);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(526, 104);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Прогноз потребления";
            // 
            // comboBoxArea
            // 
            this.comboBoxArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArea.FormattingEnabled = true;
            this.comboBoxArea.Location = new System.Drawing.Point(96, 46);
            this.comboBoxArea.Name = "comboBoxArea";
            this.comboBoxArea.Size = new System.Drawing.Size(424, 21);
            this.comboBoxArea.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Территория";
            // 
            // comboBoxShema
            // 
            this.comboBoxShema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxShema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShema.FormattingEnabled = true;
            this.comboBoxShema.Location = new System.Drawing.Point(96, 19);
            this.comboBoxShema.Name = "comboBoxShema";
            this.comboBoxShema.Size = new System.Drawing.Size(424, 21);
            this.comboBoxShema.TabIndex = 6;
            this.comboBoxShema.SelectedIndexChanged += new System.EventHandler(this.comboBoxShema_SelectedIndexChanged);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(96, 73);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(424, 21);
            this.comboBoxStatus.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Статус";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Схема";
            // 
            // PrognozOtkOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(526, 392);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "PrognozOtkOptions";
            this.Text = "Настройки отчёта отклонения от прогноза потребления";
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxCat;
        private System.Windows.Forms.CheckBox checkBoxAvg;
        private System.Windows.Forms.MaskedTextBox textBoxNameColumnOtkl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MaskedTextBox textBoxNameColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox textBoxTI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxShema;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
    }
}
