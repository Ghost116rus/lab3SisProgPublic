namespace lab1SisProg
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sourceCodeTextBox = new System.Windows.Forms.TextBox();
            this.operationCodeDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.symbolTableDataGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.settingTableDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.firstPassErrorTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.supportTableDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.binaryCodeTextBox = new System.Windows.Forms.TextBox();
            this.secondPassErrorTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.firstPassButton = new System.Windows.Forms.Button();
            this.secondPassButton = new System.Windows.Forms.Button();
            this.exampleComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operationCodeDataGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbolTableDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingTableDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supportTableDataGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sourceCodeTextBox);
            this.groupBox1.Controls.Add(this.operationCodeDataGrid);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 603);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // sourceCodeTextBox
            // 
            this.sourceCodeTextBox.BackColor = System.Drawing.Color.White;
            this.sourceCodeTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sourceCodeTextBox.Location = new System.Drawing.Point(6, 32);
            this.sourceCodeTextBox.Multiline = true;
            this.sourceCodeTextBox.Name = "sourceCodeTextBox";
            this.sourceCodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sourceCodeTextBox.Size = new System.Drawing.Size(317, 345);
            this.sourceCodeTextBox.TabIndex = 10;
            this.sourceCodeTextBox.TextChanged += new System.EventHandler(this.SourceCodeTextBox_TextChanged);
            // 
            // operationCodeDataGrid
            // 
            this.operationCodeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.operationCodeDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.operationCodeDataGrid.Location = new System.Drawing.Point(60, 409);
            this.operationCodeDataGrid.Name = "operationCodeDataGrid";
            this.operationCodeDataGrid.RowHeadersVisible = false;
            this.operationCodeDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.operationCodeDataGrid.Size = new System.Drawing.Size(202, 188);
            this.operationCodeDataGrid.TabIndex = 3;
            this.operationCodeDataGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.OperationCodeDataGrid_CellBeginEdit);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "МКО";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "Дв.Код";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Длина";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(73, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Таблица кодов операций";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(107, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Исходный текст";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.symbolTableDataGrid);
            this.groupBox2.Controls.Add(this.settingTableDataGrid);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.firstPassErrorTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.supportTableDataGrid);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(347, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 603);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // symbolTableDataGrid
            // 
            this.symbolTableDataGrid.AllowUserToAddRows = false;
            this.symbolTableDataGrid.AllowUserToDeleteRows = false;
            this.symbolTableDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symbolTableDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.symbolTableDataGrid.Location = new System.Drawing.Point(9, 336);
            this.symbolTableDataGrid.Name = "symbolTableDataGrid";
            this.symbolTableDataGrid.ReadOnly = true;
            this.symbolTableDataGrid.RowHeadersVisible = false;
            this.symbolTableDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.symbolTableDataGrid.Size = new System.Drawing.Size(237, 140);
            this.symbolTableDataGrid.TabIndex = 11;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Имя";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 55;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Адрес";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 55;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Секция";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 65;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Тип";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // settingTableDataGrid
            // 
            this.settingTableDataGrid.AllowUserToAddRows = false;
            this.settingTableDataGrid.AllowUserToDeleteRows = false;
            this.settingTableDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.settingTableDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn8});
            this.settingTableDataGrid.Location = new System.Drawing.Point(252, 336);
            this.settingTableDataGrid.Name = "settingTableDataGrid";
            this.settingTableDataGrid.ReadOnly = true;
            this.settingTableDataGrid.RowHeadersVisible = false;
            this.settingTableDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.settingTableDataGrid.Size = new System.Drawing.Size(203, 140);
            this.settingTableDataGrid.TabIndex = 10;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Адрес";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Метка";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(283, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Таблица настройки";
            // 
            // firstPassErrorTextBox
            // 
            this.firstPassErrorTextBox.BackColor = System.Drawing.Color.White;
            this.firstPassErrorTextBox.Location = new System.Drawing.Point(6, 507);
            this.firstPassErrorTextBox.Multiline = true;
            this.firstPassErrorTextBox.Name = "firstPassErrorTextBox";
            this.firstPassErrorTextBox.ReadOnly = true;
            this.firstPassErrorTextBox.Size = new System.Drawing.Size(443, 90);
            this.firstPassErrorTextBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(148, 487);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Ошибки первого прохода";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(6, 316);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(205, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Таблица символических имен";
            // 
            // supportTableDataGrid
            // 
            this.supportTableDataGrid.AllowUserToAddRows = false;
            this.supportTableDataGrid.AllowUserToDeleteRows = false;
            this.supportTableDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.supportTableDataGrid.ColumnHeadersVisible = false;
            this.supportTableDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.supportTableDataGrid.Location = new System.Drawing.Point(6, 32);
            this.supportTableDataGrid.Name = "supportTableDataGrid";
            this.supportTableDataGrid.ReadOnly = true;
            this.supportTableDataGrid.RowHeadersVisible = false;
            this.supportTableDataGrid.Size = new System.Drawing.Size(443, 274);
            this.supportTableDataGrid.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.Frozen = true;
            this.dataGridViewTextBoxColumn4.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 105;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.Frozen = true;
            this.dataGridViewTextBoxColumn5.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 105;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.Frozen = true;
            this.dataGridViewTextBoxColumn6.HeaderText = "Column3";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 105;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.Frozen = true;
            this.dataGridViewTextBoxColumn7.HeaderText = "Column4";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 105;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(140, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Вспомогательная таблица";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.binaryCodeTextBox);
            this.groupBox3.Controls.Add(this.secondPassErrorTextBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(808, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(336, 603);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // binaryCodeTextBox
            // 
            this.binaryCodeTextBox.BackColor = System.Drawing.Color.White;
            this.binaryCodeTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.binaryCodeTextBox.Location = new System.Drawing.Point(6, 32);
            this.binaryCodeTextBox.Multiline = true;
            this.binaryCodeTextBox.Name = "binaryCodeTextBox";
            this.binaryCodeTextBox.ReadOnly = true;
            this.binaryCodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.binaryCodeTextBox.Size = new System.Drawing.Size(324, 444);
            this.binaryCodeTextBox.TabIndex = 9;
            // 
            // secondPassErrorTextBox
            // 
            this.secondPassErrorTextBox.BackColor = System.Drawing.Color.White;
            this.secondPassErrorTextBox.Location = new System.Drawing.Point(6, 507);
            this.secondPassErrorTextBox.Multiline = true;
            this.secondPassErrorTextBox.Name = "secondPassErrorTextBox";
            this.secondPassErrorTextBox.ReadOnly = true;
            this.secondPassErrorTextBox.Size = new System.Drawing.Size(317, 90);
            this.secondPassErrorTextBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(81, 487);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ошибки второго прохода";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(117, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "Двоичный код";
            // 
            // firstPassButton
            // 
            this.firstPassButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.firstPassButton.Location = new System.Drawing.Point(72, 632);
            this.firstPassButton.Name = "firstPassButton";
            this.firstPassButton.Size = new System.Drawing.Size(202, 45);
            this.firstPassButton.TabIndex = 10;
            this.firstPassButton.Text = "Первый проход";
            this.firstPassButton.UseVisualStyleBackColor = true;
            this.firstPassButton.Click += new System.EventHandler(this.FirstPassButton_Click);
            // 
            // secondPassButton
            // 
            this.secondPassButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.secondPassButton.Location = new System.Drawing.Point(468, 632);
            this.secondPassButton.Name = "secondPassButton";
            this.secondPassButton.Size = new System.Drawing.Size(202, 45);
            this.secondPassButton.TabIndex = 11;
            this.secondPassButton.Text = "Второй проход";
            this.secondPassButton.UseVisualStyleBackColor = true;
            this.secondPassButton.Click += new System.EventHandler(this.SecondPassButton_Click);
            // 
            // exampleComboBox
            // 
            this.exampleComboBox.FormattingEnabled = true;
            this.exampleComboBox.Items.AddRange(new object[] {
            "Прямая адресация",
            "Относительная адресация",
            "Смешанная адресация"});
            this.exampleComboBox.Location = new System.Drawing.Point(814, 651);
            this.exampleComboBox.Name = "exampleComboBox";
            this.exampleComboBox.Size = new System.Drawing.Size(317, 21);
            this.exampleComboBox.TabIndex = 12;
            this.exampleComboBox.SelectedIndexChanged += new System.EventHandler(this.ExampleComboBox_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.Location = new System.Drawing.Point(916, 632);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "Выбор адресации";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 684);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.exampleComboBox);
            this.Controls.Add(this.secondPassButton);
            this.Controls.Add(this.firstPassButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Двухпросмотровый ассемблер для программы в полноперемещаемом формате";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.operationCodeDataGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbolTableDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingTableDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supportTableDataGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView operationCodeDataGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView supportTableDataGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstPassErrorTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox binaryCodeTextBox;
        private System.Windows.Forms.TextBox secondPassErrorTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button firstPassButton;
        private System.Windows.Forms.Button secondPassButton;
        private System.Windows.Forms.TextBox sourceCodeTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridView settingTableDataGrid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox exampleComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridView symbolTableDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}

