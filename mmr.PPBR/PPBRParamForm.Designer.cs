namespace mmr
{
    partial class PPBRParamForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PPBRParamForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ButtonSave = new System.Windows.Forms.ToolStripButton();
            this.ButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonClose = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDecimalPoint = new System.Windows.Forms.MaskedTextBox();
            this.comboBoxEncode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFilename = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBeginData = new System.Windows.Forms.MaskedTextBox();
            this.textBoxSeparator = new System.Windows.Forms.MaskedTextBox();
            this.textBoxHead = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxArh = new System.Windows.Forms.ListBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonMakeLabelTime = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonInWork = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonInArh = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listBoxWork = new System.Windows.Forms.ListBox();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelButtons.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(505, 25);
            this.toolStrip1.TabIndex = 24;
            this.toolStrip1.Text = "toolStrip1";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(7);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(505, 455);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 164);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxDecimalPoint);
            this.groupBox1.Controls.Add(this.comboBoxEncode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxFilename);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxBeginData);
            this.groupBox1.Controls.Add(this.textBoxSeparator);
            this.groupBox1.Controls.Add(this.textBoxHead);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 164);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры макета";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Десятичная точка";
            // 
            // textBoxDecimalPoint
            // 
            this.textBoxDecimalPoint.Location = new System.Drawing.Point(175, 140);
            this.textBoxDecimalPoint.Name = "textBoxDecimalPoint";
            this.textBoxDecimalPoint.Size = new System.Drawing.Size(29, 20);
            this.textBoxDecimalPoint.TabIndex = 68;
            this.textBoxDecimalPoint.Text = ".";
            this.textBoxDecimalPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDecimalPoint.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // comboBoxEncode
            // 
            this.comboBoxEncode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncode.FormattingEnabled = true;
            this.comboBoxEncode.Location = new System.Drawing.Point(175, 43);
            this.comboBoxEncode.Name = "comboBoxEncode";
            this.comboBoxEncode.Size = new System.Drawing.Size(259, 21);
            this.comboBoxEncode.TabIndex = 65;
            this.comboBoxEncode.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Кодировка макета";
            // 
            // textBoxFilename
            // 
            this.textBoxFilename.Location = new System.Drawing.Point(175, 19);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.Size = new System.Drawing.Size(259, 20);
            this.textBoxFilename.TabIndex = 64;
            this.textBoxFilename.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Шаблон имени файла";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(97, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "Разделитель";
            // 
            // textBoxBeginData
            // 
            this.textBoxBeginData.Location = new System.Drawing.Point(175, 92);
            this.textBoxBeginData.Name = "textBoxBeginData";
            this.textBoxBeginData.Size = new System.Drawing.Size(29, 20);
            this.textBoxBeginData.TabIndex = 56;
            this.textBoxBeginData.Text = "3";
            this.textBoxBeginData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxBeginData.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // textBoxSeparator
            // 
            this.textBoxSeparator.Location = new System.Drawing.Point(175, 116);
            this.textBoxSeparator.Name = "textBoxSeparator";
            this.textBoxSeparator.Size = new System.Drawing.Size(29, 20);
            this.textBoxSeparator.TabIndex = 57;
            this.textBoxSeparator.Text = ",";
            this.textBoxSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSeparator.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // textBoxHead
            // 
            this.textBoxHead.Location = new System.Drawing.Point(175, 68);
            this.textBoxHead.Name = "textBoxHead";
            this.textBoxHead.Size = new System.Drawing.Size(259, 20);
            this.textBoxHead.TabIndex = 55;
            this.textBoxHead.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(197, 13);
            this.label6.TabIndex = 60;
            this.label6.Text = "Данные начинаются с              строки";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 13);
            this.label5.TabIndex = 59;
            this.label5.Text = "Шаблон заголовка файла";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.Controls.Add(this.listBoxArh, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.panelButtons, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.listBoxWork, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 180);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(485, 278);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // listBoxArh
            // 
            this.listBoxArh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxArh.FormattingEnabled = true;
            this.listBoxArh.Location = new System.Drawing.Point(316, 21);
            this.listBoxArh.Name = "listBoxArh";
            this.listBoxArh.Size = new System.Drawing.Size(163, 251);
            this.listBoxArh.TabIndex = 13;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonAdd);
            this.panelButtons.Controls.Add(this.buttonDel);
            this.panelButtons.Controls.Add(this.buttonRename);
            this.panelButtons.Controls.Add(this.buttonMakeLabelTime);
            this.panelButtons.Controls.Add(this.buttonDown);
            this.panelButtons.Controls.Add(this.buttonInWork);
            this.panelButtons.Controls.Add(this.buttonUp);
            this.panelButtons.Controls.Add(this.buttonInArh);
            this.panelButtons.Location = new System.Drawing.Point(173, 21);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(105, 225);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.ImageIndex = 4;
            this.buttonAdd.ImageList = this.imageList;
            this.buttonAdd.Location = new System.Drawing.Point(5, 0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(94, 24);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "Up.bmp");
            this.imageList.Images.SetKeyName(1, "Delete.bmp");
            this.imageList.Images.SetKeyName(2, "Down.bmp");
            this.imageList.Images.SetKeyName(3, "Edit.bmp");
            this.imageList.Images.SetKeyName(4, "Insert.bmp");
            this.imageList.Images.SetKeyName(5, "Next.bmp");
            this.imageList.Images.SetKeyName(6, "Post.bmp");
            this.imageList.Images.SetKeyName(7, "Prev.bmp");
            // 
            // buttonDel
            // 
            this.buttonDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDel.ImageIndex = 1;
            this.buttonDel.ImageList = this.imageList;
            this.buttonDel.Location = new System.Drawing.Point(5, 26);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(94, 24);
            this.buttonDel.TabIndex = 5;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonRename
            // 
            this.buttonRename.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRename.ImageKey = "Edit.bmp";
            this.buttonRename.ImageList = this.imageList;
            this.buttonRename.Location = new System.Drawing.Point(5, 52);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(94, 24);
            this.buttonRename.TabIndex = 6;
            this.buttonRename.Text = "Сменить";
            this.buttonRename.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonMakeLabelTime
            // 
            this.buttonMakeLabelTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMakeLabelTime.ImageIndex = 6;
            this.buttonMakeLabelTime.ImageList = this.imageList;
            this.buttonMakeLabelTime.Location = new System.Drawing.Point(5, 78);
            this.buttonMakeLabelTime.Name = "buttonMakeLabelTime";
            this.buttonMakeLabelTime.Size = new System.Drawing.Size(94, 24);
            this.buttonMakeLabelTime.TabIndex = 7;
            this.buttonMakeLabelTime.Text = "Время";
            this.buttonMakeLabelTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMakeLabelTime.UseVisualStyleBackColor = true;
            this.buttonMakeLabelTime.Click += new System.EventHandler(this.buttonMakeLabelTime_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDown.ImageIndex = 2;
            this.buttonDown.ImageList = this.imageList;
            this.buttonDown.Location = new System.Drawing.Point(5, 138);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(94, 24);
            this.buttonDown.TabIndex = 9;
            this.buttonDown.Text = "Вниз";
            this.buttonDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonInWork
            // 
            this.buttonInWork.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInWork.ImageKey = "Prev.bmp";
            this.buttonInWork.ImageList = this.imageList;
            this.buttonInWork.Location = new System.Drawing.Point(5, 201);
            this.buttonInWork.Name = "buttonInWork";
            this.buttonInWork.Size = new System.Drawing.Size(94, 24);
            this.buttonInWork.TabIndex = 11;
            this.buttonInWork.Text = "В работу";
            this.buttonInWork.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonInWork.UseVisualStyleBackColor = true;
            this.buttonInWork.Click += new System.EventHandler(this.buttonInWork_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUp.ImageIndex = 0;
            this.buttonUp.ImageList = this.imageList;
            this.buttonUp.Location = new System.Drawing.Point(5, 112);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(94, 24);
            this.buttonUp.TabIndex = 8;
            this.buttonUp.Text = "Вверх";
            this.buttonUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonInArh
            // 
            this.buttonInArh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInArh.ImageIndex = 5;
            this.buttonInArh.ImageList = this.imageList;
            this.buttonInArh.Location = new System.Drawing.Point(5, 175);
            this.buttonInArh.Name = "buttonInArh";
            this.buttonInArh.Size = new System.Drawing.Size(94, 24);
            this.buttonInArh.TabIndex = 10;
            this.buttonInArh.Text = "В архив";
            this.buttonInArh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonInArh.UseVisualStyleBackColor = true;
            this.buttonInArh.Click += new System.EventHandler(this.buttonInArh_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(316, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 73;
            this.label8.Text = "Архивные параметры";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 69;
            this.label7.Text = "Рабочие параметры";
            // 
            // listBoxWork
            // 
            this.listBoxWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxWork.FormattingEnabled = true;
            this.listBoxWork.Location = new System.Drawing.Point(6, 21);
            this.listBoxWork.Name = "listBoxWork";
            this.listBoxWork.Size = new System.Drawing.Size(161, 251);
            this.listBoxWork.TabIndex = 74;
            // 
            // PPBRParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(505, 480);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PPBRParamForm";
            this.ShowInTaskbar = false;
            this.Text = "Параметры приёма ППБР";
            this.Load += new System.EventHandler(this.PPBRParamForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ButtonSave;
        private System.Windows.Forms.ToolStripButton ButtonCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ButtonClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonInWork;
        private System.Windows.Forms.Button buttonMakeLabelTime;
        private System.Windows.Forms.Button buttonInArh;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.ListBox listBoxArh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBoxWork;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox textBoxBeginData;
        private System.Windows.Forms.MaskedTextBox textBoxSeparator;
        private System.Windows.Forms.MaskedTextBox textBoxHead;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxEncode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox textBoxFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox textBoxDecimalPoint;
    }
}
