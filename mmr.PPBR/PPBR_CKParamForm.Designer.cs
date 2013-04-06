namespace mmr
{
    partial class PPBR_CKParamForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PPBR_CKParamForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonClose = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDecimalPoint = new System.Windows.Forms.MaskedTextBox();
            this.buttonSelectDirectory = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.textBoxDirectory = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxEncode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFilename = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEndData = new System.Windows.Forms.MaskedTextBox();
            this.textBoxSeparator = new System.Windows.Forms.MaskedTextBox();
            this.textBoxHeader = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gridViewCKParam = new System.Windows.Forms.DataGridView();
            this.codenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parameterIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tblPPBRParametersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetPPBR = new mmr.DataSetPPBR();
            this.OnlyHourse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.controlSummaDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tblPPBRCKParametersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblPPBR_CKParametersTableAdapter = new mmr.DataSetPPBRTableAdapters.tblPPBR_CKParametersTableAdapter();
            this.tblPPBR_ParametersTableAdapter = new mmr.DataSetPPBRTableAdapters.tblPPBR_ParametersTableAdapter();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCKParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPPBRParametersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPPBR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPPBRCKParametersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSave,
            this.buttonCancel,
            this.toolStripSeparator1,
            this.buttonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(678, 25);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::mmr.Properties.Resources.save;
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(138, 22);
            this.buttonSave.Text = "Сохранить изменения";
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Image = global::mmr.Properties.Resources.Refresh;
            this.buttonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(133, 22);
            this.buttonCancel.Text = "Отменить изменения";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonClose
            // 
            this.buttonClose.Image = global::mmr.Properties.Resources.Cancel;
            this.buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(71, 22);
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(678, 426);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxDecimalPoint);
            this.groupBox1.Controls.Add(this.buttonSelectDirectory);
            this.groupBox1.Controls.Add(this.textBoxDirectory);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxEncode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxFilename);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxEndData);
            this.groupBox1.Controls.Add(this.textBoxSeparator);
            this.groupBox1.Controls.Add(this.textBoxHeader);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 204);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры макета  СК-2003";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(55, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 68;
            this.label7.Text = "Десятичная точка";
            // 
            // textBoxDecimalPoint
            // 
            this.textBoxDecimalPoint.Location = new System.Drawing.Point(160, 177);
            this.textBoxDecimalPoint.Name = "textBoxDecimalPoint";
            this.textBoxDecimalPoint.Size = new System.Drawing.Size(44, 20);
            this.textBoxDecimalPoint.TabIndex = 67;
            this.textBoxDecimalPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDecimalPoint.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // buttonSelectDirectory
            // 
            this.buttonSelectDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectDirectory.ImageIndex = 3;
            this.buttonSelectDirectory.ImageList = this.imageList;
            this.buttonSelectDirectory.Location = new System.Drawing.Point(614, 23);
            this.buttonSelectDirectory.Name = "buttonSelectDirectory";
            this.buttonSelectDirectory.Size = new System.Drawing.Size(26, 23);
            this.buttonSelectDirectory.TabIndex = 1;
            this.buttonSelectDirectory.UseVisualStyleBackColor = true;
            this.buttonSelectDirectory.Click += new System.EventHandler(this.buttonSelectDirectory_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "Edit.bmp");
            this.imageList.Images.SetKeyName(1, "Insert.bmp");
            this.imageList.Images.SetKeyName(2, "Delete.bmp");
            this.imageList.Images.SetKeyName(3, "folder.bmp");
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDirectory.Location = new System.Drawing.Point(160, 25);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(448, 20);
            this.textBoxDirectory.TabIndex = 0;
            this.textBoxDirectory.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "Папка назначения";
            // 
            // comboBoxEncode
            // 
            this.comboBoxEncode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncode.FormattingEnabled = true;
            this.comboBoxEncode.Location = new System.Drawing.Point(160, 77);
            this.comboBoxEncode.Name = "comboBoxEncode";
            this.comboBoxEncode.Size = new System.Drawing.Size(208, 21);
            this.comboBoxEncode.TabIndex = 3;
            this.comboBoxEncode.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Кодировка макета";
            // 
            // textBoxFilename
            // 
            this.textBoxFilename.Location = new System.Drawing.Point(160, 51);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.Size = new System.Drawing.Size(208, 20);
            this.textBoxFilename.TabIndex = 2;
            this.textBoxFilename.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Шаблон имени файла";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "Разделитель";
            // 
            // textBoxEndData
            // 
            this.textBoxEndData.Location = new System.Drawing.Point(160, 128);
            this.textBoxEndData.Name = "textBoxEndData";
            this.textBoxEndData.Size = new System.Drawing.Size(208, 20);
            this.textBoxEndData.TabIndex = 5;
            this.textBoxEndData.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // textBoxSeparator
            // 
            this.textBoxSeparator.Location = new System.Drawing.Point(160, 153);
            this.textBoxSeparator.Name = "textBoxSeparator";
            this.textBoxSeparator.Size = new System.Drawing.Size(44, 20);
            this.textBoxSeparator.TabIndex = 6;
            this.textBoxSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSeparator.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // textBoxHeader
            // 
            this.textBoxHeader.Location = new System.Drawing.Point(160, 103);
            this.textBoxHeader.Name = "textBoxHeader";
            this.textBoxHeader.Size = new System.Drawing.Size(208, 20);
            this.textBoxHeader.TabIndex = 4;
            this.textBoxHeader.TextChanged += new System.EventHandler(this.Parameters_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 60;
            this.label6.Text = "Концовка макета";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 13);
            this.label5.TabIndex = 59;
            this.label5.Text = "Шаблон заголовка макета";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.gridViewCKParam);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(13, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(652, 190);
            this.groupBox2.TabIndex = 57;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Данные передаваемые в макете ";
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.ImageIndex = 0;
            this.button3.ImageList = this.imageList;
            this.button3.Location = new System.Drawing.Point(183, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 26);
            this.button3.TabIndex = 3;
            this.button3.Text = "Изменить";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.ImageIndex = 2;
            this.button2.ImageList = this.imageList;
            this.button2.Location = new System.Drawing.Point(94, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 26);
            this.button2.TabIndex = 2;
            this.button2.Text = "Удалить";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageIndex = 1;
            this.button1.ImageList = this.imageList;
            this.button1.Location = new System.Drawing.Point(5, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridViewCKParam
            // 
            this.gridViewCKParam.AllowUserToAddRows = false;
            this.gridViewCKParam.AllowUserToDeleteRows = false;
            this.gridViewCKParam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridViewCKParam.AutoGenerateColumns = false;
            this.gridViewCKParam.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewCKParam.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridViewCKParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewCKParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codenameDataGridViewTextBoxColumn,
            this.parameterIDDataGridViewTextBoxColumn,
            this.OnlyHourse,
            this.controlSummaDataGridViewCheckBoxColumn});
            this.gridViewCKParam.DataSource = this.tblPPBRCKParametersBindingSource;
            this.gridViewCKParam.Location = new System.Drawing.Point(5, 39);
            this.gridViewCKParam.MultiSelect = false;
            this.gridViewCKParam.Name = "gridViewCKParam";
            this.gridViewCKParam.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewCKParam.Size = new System.Drawing.Size(642, 146);
            this.gridViewCKParam.TabIndex = 0;
            this.gridViewCKParam.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridViewCKParam_CellBeginEdit);
            this.gridViewCKParam.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridViewCKParam_RowValidating);
            this.gridViewCKParam.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gridViewCKParam_CellValueNeeded);
            this.gridViewCKParam.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridViewCKParam_UserDeletedRow);
            this.gridViewCKParam.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewCKParam_RowValidated);
            // 
            // codenameDataGridViewTextBoxColumn
            // 
            this.codenameDataGridViewTextBoxColumn.DataPropertyName = "Codename";
            this.codenameDataGridViewTextBoxColumn.FillWeight = 101.5228F;
            this.codenameDataGridViewTextBoxColumn.HeaderText = "Код";
            this.codenameDataGridViewTextBoxColumn.Name = "codenameDataGridViewTextBoxColumn";
            // 
            // parameterIDDataGridViewTextBoxColumn
            // 
            this.parameterIDDataGridViewTextBoxColumn.DataPropertyName = "ParameterID";
            this.parameterIDDataGridViewTextBoxColumn.DataSource = this.tblPPBRParametersBindingSource;
            this.parameterIDDataGridViewTextBoxColumn.DisplayMember = "Name";
            this.parameterIDDataGridViewTextBoxColumn.FillWeight = 99.49239F;
            this.parameterIDDataGridViewTextBoxColumn.HeaderText = "Параметр";
            this.parameterIDDataGridViewTextBoxColumn.Name = "parameterIDDataGridViewTextBoxColumn";
            this.parameterIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.parameterIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.parameterIDDataGridViewTextBoxColumn.ValueMember = "ID";
            // 
            // tblPPBRParametersBindingSource
            // 
            this.tblPPBRParametersBindingSource.DataMember = "tblPPBR_Parameters";
            this.tblPPBRParametersBindingSource.DataSource = this.dataSetPPBR;
            this.tblPPBRParametersBindingSource.Filter = "IsLabelTime=false";
            // 
            // dataSetPPBR
            // 
            this.dataSetPPBR.DataSetName = "DataSetPPBR";
            this.dataSetPPBR.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // OnlyHourse
            // 
            this.OnlyHourse.DataPropertyName = "OnlyHourse";
            this.OnlyHourse.HeaderText = "Часовые значения";
            this.OnlyHourse.Name = "OnlyHourse";
            // 
            // controlSummaDataGridViewCheckBoxColumn
            // 
            this.controlSummaDataGridViewCheckBoxColumn.DataPropertyName = "ControlSumma";
            this.controlSummaDataGridViewCheckBoxColumn.FalseValue = "false";
            this.controlSummaDataGridViewCheckBoxColumn.FillWeight = 99.49239F;
            this.controlSummaDataGridViewCheckBoxColumn.HeaderText = "Контрольная сумма";
            this.controlSummaDataGridViewCheckBoxColumn.IndeterminateValue = "false";
            this.controlSummaDataGridViewCheckBoxColumn.Name = "controlSummaDataGridViewCheckBoxColumn";
            this.controlSummaDataGridViewCheckBoxColumn.TrueValue = "true";
            // 
            // tblPPBRCKParametersBindingSource
            // 
            this.tblPPBRCKParametersBindingSource.DataMember = "tblPPBR_CKParameters";
            this.tblPPBRCKParametersBindingSource.DataSource = this.dataSetPPBR;
            // 
            // tblPPBR_CKParametersTableAdapter
            // 
            this.tblPPBR_CKParametersTableAdapter.ClearBeforeFill = true;
            // 
            // tblPPBR_ParametersTableAdapter
            // 
            this.tblPPBR_ParametersTableAdapter.ClearBeforeFill = true;
            // 
            // PPBR_CKParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(678, 451);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PPBR_CKParamForm";
            this.Text = "Параметры макета СК-2003";
            this.Load += new System.EventHandler(this.PPBR_CKParamForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCKParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPPBRParametersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPPBR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblPPBRCKParametersBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.ToolStripButton buttonCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox textBoxEndData;
        private System.Windows.Forms.MaskedTextBox textBoxSeparator;
        private System.Windows.Forms.MaskedTextBox textBoxHeader;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxEncode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox textBoxFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox textBoxDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSelectDirectory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gridViewCKParam;
        private DataSetPPBR dataSetPPBR;
        private System.Windows.Forms.BindingSource tblPPBRCKParametersBindingSource;
        private mmr.DataSetPPBRTableAdapters.tblPPBR_CKParametersTableAdapter tblPPBR_CKParametersTableAdapter;
        private System.Windows.Forms.BindingSource tblPPBRParametersBindingSource;
        private mmr.DataSetPPBRTableAdapters.tblPPBR_ParametersTableAdapter tblPPBR_ParametersTableAdapter;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn codenameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn parameterIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OnlyHourse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn controlSummaDataGridViewCheckBoxColumn;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox textBoxDecimalPoint;
    }
}
