namespace mmr
{
    partial class PPBREmailForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxTimeOut = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxReply = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxSender = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSMTPPort = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSMTP = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxIsDeleted = new System.Windows.Forms.CheckBox();
            this.textBoxPas = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxPOPPort = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPOP = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonSave = new System.Windows.Forms.ToolStripButton();
            this.ButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.ButtonClose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonSave,
            this.ButtonCancel,
            this.toolStripSeparator1,
            this.ButtonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(529, 25);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(529, 423);
            this.panel1.TabIndex = 26;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.textBoxTimeOut);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.textBoxSubject);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(10, 285);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(509, 77);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Общие ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(143, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 62;
            this.label10.Text = "секунд";
            // 
            // textBoxTimeOut
            // 
            this.textBoxTimeOut.Location = new System.Drawing.Point(98, 45);
            this.textBoxTimeOut.Name = "textBoxTimeOut";
            this.textBoxTimeOut.Size = new System.Drawing.Size(39, 20);
            this.textBoxTimeOut.TabIndex = 58;
            this.textBoxTimeOut.Tag = "";
            this.textBoxTimeOut.Text = "60";
            this.textBoxTimeOut.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 61;
            this.label9.Text = "Ожидание";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSubject.Location = new System.Drawing.Point(98, 18);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(407, 20);
            this.textBoxSubject.TabIndex = 59;
            this.textBoxSubject.Tag = "";
            this.textBoxSubject.Text = "График ППБР на 24 часа";
            this.textBoxSubject.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Тема письма";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxReply);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBoxSender);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxSMTPPort);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxSMTP);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(10, 158);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 127);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Отправка почты";
            // 
            // textBoxReply
            // 
            this.textBoxReply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReply.Location = new System.Drawing.Point(98, 94);
            this.textBoxReply.Name = "textBoxReply";
            this.textBoxReply.Size = new System.Drawing.Size(405, 20);
            this.textBoxReply.TabIndex = 45;
            this.textBoxReply.Tag = "";
            this.textBoxReply.Text = "ex-mrdv";
            this.textBoxReply.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "Кому выслать";
            // 
            // textBoxSender
            // 
            this.textBoxSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSender.Location = new System.Drawing.Point(98, 68);
            this.textBoxSender.Name = "textBoxSender";
            this.textBoxSender.Size = new System.Drawing.Size(405, 20);
            this.textBoxSender.TabIndex = 43;
            this.textBoxSender.Tag = "";
            this.textBoxSender.Text = "ex-mrdv";
            this.textBoxSender.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "От кого";
            // 
            // textBoxSMTPPort
            // 
            this.textBoxSMTPPort.Location = new System.Drawing.Point(98, 42);
            this.textBoxSMTPPort.Name = "textBoxSMTPPort";
            this.textBoxSMTPPort.Size = new System.Drawing.Size(90, 20);
            this.textBoxSMTPPort.TabIndex = 3;
            this.textBoxSMTPPort.Tag = "";
            this.textBoxSMTPPort.Text = "25";
            this.textBoxSMTPPort.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Порт SMTP";
            // 
            // textBoxSMTP
            // 
            this.textBoxSMTP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSMTP.Location = new System.Drawing.Point(98, 16);
            this.textBoxSMTP.Name = "textBoxSMTP";
            this.textBoxSMTP.Size = new System.Drawing.Size(405, 20);
            this.textBoxSMTP.TabIndex = 2;
            this.textBoxSMTP.Tag = "";
            this.textBoxSMTP.Text = "ex-mrdv";
            this.textBoxSMTP.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "SMTP-сервер";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxIsDeleted);
            this.groupBox2.Controls.Add(this.textBoxPas);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxPOPPort);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxPOP);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxEmail);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(509, 148);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Получение почты";
            // 
            // checkBoxIsDeleted
            // 
            this.checkBoxIsDeleted.AutoSize = true;
            this.checkBoxIsDeleted.Location = new System.Drawing.Point(96, 125);
            this.checkBoxIsDeleted.Name = "checkBoxIsDeleted";
            this.checkBoxIsDeleted.Size = new System.Drawing.Size(232, 17);
            this.checkBoxIsDeleted.TabIndex = 51;
            this.checkBoxIsDeleted.Text = "Удалить письмо после загрузки макета";
            this.checkBoxIsDeleted.UseVisualStyleBackColor = true;
            this.checkBoxIsDeleted.CheckedChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // textBoxPas
            // 
            this.textBoxPas.Location = new System.Drawing.Point(96, 100);
            this.textBoxPas.Name = "textBoxPas";
            this.textBoxPas.PasswordChar = '@';
            this.textBoxPas.Size = new System.Drawing.Size(199, 20);
            this.textBoxPas.TabIndex = 8;
            this.textBoxPas.Tag = "";
            this.textBoxPas.Text = "пасворд";
            this.textBoxPas.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Пароль";
            // 
            // textBoxPOPPort
            // 
            this.textBoxPOPPort.Location = new System.Drawing.Point(96, 46);
            this.textBoxPOPPort.Name = "textBoxPOPPort";
            this.textBoxPOPPort.Size = new System.Drawing.Size(92, 20);
            this.textBoxPOPPort.TabIndex = 6;
            this.textBoxPOPPort.Tag = "";
            this.textBoxPOPPort.Text = "110";
            this.textBoxPOPPort.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Порт POP3";
            // 
            // textBoxPOP
            // 
            this.textBoxPOP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPOP.Location = new System.Drawing.Point(96, 19);
            this.textBoxPOP.Name = "textBoxPOP";
            this.textBoxPOP.Size = new System.Drawing.Size(407, 20);
            this.textBoxPOP.TabIndex = 5;
            this.textBoxPOP.Tag = "";
            this.textBoxPOP.Text = "ex-mrdv";
            this.textBoxPOP.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "POP3-сервер";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(96, 73);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(199, 20);
            this.textBoxEmail.TabIndex = 7;
            this.textBoxEmail.Tag = "";
            this.textBoxEmail.Text = "oper@rdurm.odusv.so-ups.ru";
            this.textBoxEmail.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Почта";
            // 
            // ButtonSave
            // 
            this.ButtonSave.Image = global::mmr.Properties.Resources.save;
            this.ButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(138, 22);
            this.ButtonSave.Text = "Сохранить изменения";
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Image = global::mmr.Properties.Resources.Refresh;
            this.ButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(133, 22);
            this.ButtonCancel.Text = "Отменить изменения";
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Image = global::mmr.Properties.Resources.Cancel;
            this.ButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(71, 22);
            this.ButtonClose.Text = "Закрыть";
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // PPBREmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(529, 448);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PPBREmailForm";
            this.Text = "Настройки приёма ППБР из почты";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ButtonSave;
        private System.Windows.Forms.ToolStripButton ButtonCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ButtonClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox textBoxPas;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox textBoxPOPPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox textBoxPOP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox textBoxEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox textBoxSMTPPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox textBoxSMTP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox textBoxReply;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MaskedTextBox textBoxSender;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MaskedTextBox textBoxTimeOut;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox textBoxSubject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxIsDeleted;
    }
}
