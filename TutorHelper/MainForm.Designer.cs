namespace TutorHelper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlTutorHelper = new System.Windows.Forms.TabControl();
            this.tabPageInvoices = new System.Windows.Forms.TabPage();
            this.dataGridViewInvoicesLastMonth = new System.Windows.Forms.DataGridView();
            this.buttonCancelChangesInv = new System.Windows.Forms.Button();
            this.buttonSendInvoices = new System.Windows.Forms.Button();
            this.dataGridViewInvoices = new System.Windows.Forms.DataGridView();
            this.tabPageLink = new System.Windows.Forms.TabPage();
            this.buttonCancelChangesLink = new System.Windows.Forms.Button();
            this.buttonSaveLink = new System.Windows.Forms.Button();
            this.radioButtonShowHistorical = new System.Windows.Forms.RadioButton();
            this.radioButtonShowAllLink = new System.Windows.Forms.RadioButton();
            this.radioButtonShowActual = new System.Windows.Forms.RadioButton();
            this.dataGridViewStudLessonLink = new System.Windows.Forms.DataGridView();
            this.tabPageStudents = new System.Windows.Forms.TabPage();
            this.radioButtonShowAll = new System.Windows.Forms.RadioButton();
            this.radioButtonShowCurrent = new System.Windows.Forms.RadioButton();
            this.buttonCancelStudentsChanges = new System.Windows.Forms.Button();
            this.buttonSaveStudents = new System.Windows.Forms.Button();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.tabPageLessons = new System.Windows.Forms.TabPage();
            this.buttonCancelLessonsChanges = new System.Windows.Forms.Button();
            this.buttonSaveLessons = new System.Windows.Forms.Button();
            this.dataGridViewLessons = new System.Windows.Forms.DataGridView();
            this.tabPageAllInvoices = new System.Windows.Forms.TabPage();
            this.dataGridViewAllInvoices = new System.Windows.Forms.DataGridView();
            this.tabControlTutorHelper.SuspendLayout();
            this.tabPageInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInvoicesLastMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInvoices)).BeginInit();
            this.tabPageLink.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudLessonLink)).BeginInit();
            this.tabPageStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.tabPageLessons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLessons)).BeginInit();
            this.tabPageAllInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllInvoices)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlTutorHelper
            // 
            this.tabControlTutorHelper.Controls.Add(this.tabPageInvoices);
            this.tabControlTutorHelper.Controls.Add(this.tabPageLink);
            this.tabControlTutorHelper.Controls.Add(this.tabPageStudents);
            this.tabControlTutorHelper.Controls.Add(this.tabPageLessons);
            this.tabControlTutorHelper.Controls.Add(this.tabPageAllInvoices);
            this.tabControlTutorHelper.Location = new System.Drawing.Point(15, 8);
            this.tabControlTutorHelper.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlTutorHelper.Name = "tabControlTutorHelper";
            this.tabControlTutorHelper.SelectedIndex = 0;
            this.tabControlTutorHelper.Size = new System.Drawing.Size(818, 461);
            this.tabControlTutorHelper.TabIndex = 3;
            this.tabControlTutorHelper.SelectedIndexChanged += new System.EventHandler(this.tabControlTutorHelper_SelectedIndexChanged);
            // 
            // tabPageInvoices
            // 
            this.tabPageInvoices.Controls.Add(this.dataGridViewInvoicesLastMonth);
            this.tabPageInvoices.Controls.Add(this.buttonCancelChangesInv);
            this.tabPageInvoices.Controls.Add(this.buttonSendInvoices);
            this.tabPageInvoices.Controls.Add(this.dataGridViewInvoices);
            this.tabPageInvoices.Location = new System.Drawing.Point(4, 24);
            this.tabPageInvoices.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageInvoices.Name = "tabPageInvoices";
            this.tabPageInvoices.Size = new System.Drawing.Size(810, 433);
            this.tabPageInvoices.TabIndex = 4;
            this.tabPageInvoices.Text = "Invoices";
            this.tabPageInvoices.UseVisualStyleBackColor = true;
            // 
            // dataGridViewInvoicesLastMonth
            // 
            this.dataGridViewInvoicesLastMonth.AllowUserToAddRows = false;
            this.dataGridViewInvoicesLastMonth.AllowUserToDeleteRows = false;
            this.dataGridViewInvoicesLastMonth.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewInvoicesLastMonth.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInvoicesLastMonth.Location = new System.Drawing.Point(14, 294);
            this.dataGridViewInvoicesLastMonth.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewInvoicesLastMonth.Name = "dataGridViewInvoicesLastMonth";
            this.dataGridViewInvoicesLastMonth.ReadOnly = true;
            this.dataGridViewInvoicesLastMonth.RowHeadersWidth = 62;
            this.dataGridViewInvoicesLastMonth.RowTemplate.Height = 33;
            this.dataGridViewInvoicesLastMonth.Size = new System.Drawing.Size(780, 133);
            this.dataGridViewInvoicesLastMonth.TabIndex = 5;
            this.dataGridViewInvoicesLastMonth.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewInvoicesLastMonth_DataBindingComplete);
            // 
            // buttonCancelChangesInv
            // 
            this.buttonCancelChangesInv.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonCancelChangesInv.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonCancelChangesInv.FlatAppearance.BorderSize = 2;
            this.buttonCancelChangesInv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelChangesInv.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancelChangesInv.ForeColor = System.Drawing.Color.Black;
            this.buttonCancelChangesInv.Location = new System.Drawing.Point(664, 13);
            this.buttonCancelChangesInv.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancelChangesInv.Name = "buttonCancelChangesInv";
            this.buttonCancelChangesInv.Size = new System.Drawing.Size(130, 30);
            this.buttonCancelChangesInv.TabIndex = 4;
            this.buttonCancelChangesInv.Text = "Clean changes";
            this.buttonCancelChangesInv.UseVisualStyleBackColor = false;
            this.buttonCancelChangesInv.Click += new System.EventHandler(this.buttonCancelChangesInv_Click);
            // 
            // buttonSendInvoices
            // 
            this.buttonSendInvoices.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonSendInvoices.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSendInvoices.FlatAppearance.BorderSize = 2;
            this.buttonSendInvoices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSendInvoices.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSendInvoices.ForeColor = System.Drawing.Color.Black;
            this.buttonSendInvoices.Location = new System.Drawing.Point(521, 13);
            this.buttonSendInvoices.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSendInvoices.Name = "buttonSendInvoices";
            this.buttonSendInvoices.Size = new System.Drawing.Size(130, 30);
            this.buttonSendInvoices.TabIndex = 3;
            this.buttonSendInvoices.Text = "Send Invoices";
            this.buttonSendInvoices.UseVisualStyleBackColor = false;
            this.buttonSendInvoices.Click += new System.EventHandler(this.buttonSendInvoices_Click);
            // 
            // dataGridViewInvoices
            // 
            this.dataGridViewInvoices.AllowUserToAddRows = false;
            this.dataGridViewInvoices.AllowUserToDeleteRows = false;
            this.dataGridViewInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInvoices.Location = new System.Drawing.Point(14, 61);
            this.dataGridViewInvoices.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewInvoices.Name = "dataGridViewInvoices";
            this.dataGridViewInvoices.RowHeadersWidth = 62;
            this.dataGridViewInvoices.RowTemplate.Height = 33;
            this.dataGridViewInvoices.Size = new System.Drawing.Size(780, 218);
            this.dataGridViewInvoices.TabIndex = 1;
            this.dataGridViewInvoices.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewInvoices_DataBindingComplete);
            // 
            // tabPageLink
            // 
            this.tabPageLink.Controls.Add(this.buttonCancelChangesLink);
            this.tabPageLink.Controls.Add(this.buttonSaveLink);
            this.tabPageLink.Controls.Add(this.radioButtonShowHistorical);
            this.tabPageLink.Controls.Add(this.radioButtonShowAllLink);
            this.tabPageLink.Controls.Add(this.radioButtonShowActual);
            this.tabPageLink.Controls.Add(this.dataGridViewStudLessonLink);
            this.tabPageLink.Location = new System.Drawing.Point(4, 24);
            this.tabPageLink.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageLink.Name = "tabPageLink";
            this.tabPageLink.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageLink.Size = new System.Drawing.Size(810, 433);
            this.tabPageLink.TabIndex = 1;
            this.tabPageLink.Text = "StudentLessonLink";
            this.tabPageLink.UseVisualStyleBackColor = true;
            // 
            // buttonCancelChangesLink
            // 
            this.buttonCancelChangesLink.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonCancelChangesLink.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonCancelChangesLink.FlatAppearance.BorderSize = 2;
            this.buttonCancelChangesLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelChangesLink.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancelChangesLink.ForeColor = System.Drawing.Color.Black;
            this.buttonCancelChangesLink.Location = new System.Drawing.Point(666, 10);
            this.buttonCancelChangesLink.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancelChangesLink.Name = "buttonCancelChangesLink";
            this.buttonCancelChangesLink.Size = new System.Drawing.Size(130, 30);
            this.buttonCancelChangesLink.TabIndex = 10;
            this.buttonCancelChangesLink.Text = "Clean changes";
            this.buttonCancelChangesLink.UseVisualStyleBackColor = false;
            this.buttonCancelChangesLink.Click += new System.EventHandler(this.buttonCancelChangesLink_Click);
            // 
            // buttonSaveLink
            // 
            this.buttonSaveLink.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonSaveLink.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSaveLink.FlatAppearance.BorderSize = 2;
            this.buttonSaveLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveLink.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSaveLink.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveLink.Location = new System.Drawing.Point(523, 10);
            this.buttonSaveLink.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveLink.Name = "buttonSaveLink";
            this.buttonSaveLink.Size = new System.Drawing.Size(130, 30);
            this.buttonSaveLink.TabIndex = 9;
            this.buttonSaveLink.Text = "Save";
            this.buttonSaveLink.UseVisualStyleBackColor = false;
            this.buttonSaveLink.Click += new System.EventHandler(this.buttonSaveLink_Click);
            // 
            // radioButtonShowHistorical
            // 
            this.radioButtonShowHistorical.AutoSize = true;
            this.radioButtonShowHistorical.Location = new System.Drawing.Point(190, 10);
            this.radioButtonShowHistorical.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonShowHistorical.Name = "radioButtonShowHistorical";
            this.radioButtonShowHistorical.Size = new System.Drawing.Size(107, 19);
            this.radioButtonShowHistorical.TabIndex = 8;
            this.radioButtonShowHistorical.TabStop = true;
            this.radioButtonShowHistorical.Text = "Show Historical";
            this.radioButtonShowHistorical.UseVisualStyleBackColor = true;
            this.radioButtonShowHistorical.CheckedChanged += new System.EventHandler(this.radioButtonShowHistorical_CheckedChanged);
            // 
            // radioButtonShowAllLink
            // 
            this.radioButtonShowAllLink.AutoSize = true;
            this.radioButtonShowAllLink.Location = new System.Drawing.Point(16, 31);
            this.radioButtonShowAllLink.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonShowAllLink.Name = "radioButtonShowAllLink";
            this.radioButtonShowAllLink.Size = new System.Drawing.Size(71, 19);
            this.radioButtonShowAllLink.TabIndex = 7;
            this.radioButtonShowAllLink.TabStop = true;
            this.radioButtonShowAllLink.Text = "Show All";
            this.radioButtonShowAllLink.UseVisualStyleBackColor = true;
            this.radioButtonShowAllLink.CheckedChanged += new System.EventHandler(this.radioButtonShowAllLink_CheckedChanged);
            // 
            // radioButtonShowActual
            // 
            this.radioButtonShowActual.AutoSize = true;
            this.radioButtonShowActual.Location = new System.Drawing.Point(16, 10);
            this.radioButtonShowActual.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonShowActual.Name = "radioButtonShowActual";
            this.radioButtonShowActual.Size = new System.Drawing.Size(119, 19);
            this.radioButtonShowActual.TabIndex = 6;
            this.radioButtonShowActual.TabStop = true;
            this.radioButtonShowActual.Text = "Show Actual Only";
            this.radioButtonShowActual.UseVisualStyleBackColor = true;
            this.radioButtonShowActual.CheckedChanged += new System.EventHandler(this.radioButtonShowActual_CheckedChanged);
            // 
            // dataGridViewStudLessonLink
            // 
            this.dataGridViewStudLessonLink.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStudLessonLink.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudLessonLink.Location = new System.Drawing.Point(16, 58);
            this.dataGridViewStudLessonLink.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewStudLessonLink.Name = "dataGridViewStudLessonLink";
            this.dataGridViewStudLessonLink.RowHeadersWidth = 62;
            this.dataGridViewStudLessonLink.RowTemplate.Height = 33;
            this.dataGridViewStudLessonLink.Size = new System.Drawing.Size(780, 364);
            this.dataGridViewStudLessonLink.TabIndex = 1;
            this.dataGridViewStudLessonLink.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewStudLessonLink_DataBindingComplete);
            // 
            // tabPageStudents
            // 
            this.tabPageStudents.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageStudents.Controls.Add(this.radioButtonShowAll);
            this.tabPageStudents.Controls.Add(this.radioButtonShowCurrent);
            this.tabPageStudents.Controls.Add(this.buttonCancelStudentsChanges);
            this.tabPageStudents.Controls.Add(this.buttonSaveStudents);
            this.tabPageStudents.Controls.Add(this.dataGridViewStudents);
            this.tabPageStudents.Location = new System.Drawing.Point(4, 24);
            this.tabPageStudents.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageStudents.Name = "tabPageStudents";
            this.tabPageStudents.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageStudents.Size = new System.Drawing.Size(810, 433);
            this.tabPageStudents.TabIndex = 0;
            this.tabPageStudents.Text = "Students";
            this.tabPageStudents.Click += new System.EventHandler(this.tabPageStudents_Click);
            // 
            // radioButtonShowAll
            // 
            this.radioButtonShowAll.AutoSize = true;
            this.radioButtonShowAll.Location = new System.Drawing.Point(16, 34);
            this.radioButtonShowAll.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonShowAll.Name = "radioButtonShowAll";
            this.radioButtonShowAll.Size = new System.Drawing.Size(71, 19);
            this.radioButtonShowAll.TabIndex = 5;
            this.radioButtonShowAll.TabStop = true;
            this.radioButtonShowAll.Text = "Show All";
            this.radioButtonShowAll.UseVisualStyleBackColor = true;
            this.radioButtonShowAll.CheckedChanged += new System.EventHandler(this.radioButtonShowAll_CheckedChanged);
            // 
            // radioButtonShowCurrent
            // 
            this.radioButtonShowCurrent.AutoSize = true;
            this.radioButtonShowCurrent.Location = new System.Drawing.Point(16, 13);
            this.radioButtonShowCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonShowCurrent.Name = "radioButtonShowCurrent";
            this.radioButtonShowCurrent.Size = new System.Drawing.Size(125, 19);
            this.radioButtonShowCurrent.TabIndex = 4;
            this.radioButtonShowCurrent.TabStop = true;
            this.radioButtonShowCurrent.Text = "Show Current Only";
            this.radioButtonShowCurrent.UseVisualStyleBackColor = true;
            this.radioButtonShowCurrent.CheckedChanged += new System.EventHandler(this.radioButtonShowCurrent_CheckedChanged);
            // 
            // buttonCancelStudentsChanges
            // 
            this.buttonCancelStudentsChanges.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonCancelStudentsChanges.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonCancelStudentsChanges.FlatAppearance.BorderSize = 2;
            this.buttonCancelStudentsChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelStudentsChanges.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancelStudentsChanges.ForeColor = System.Drawing.Color.Black;
            this.buttonCancelStudentsChanges.Location = new System.Drawing.Point(666, 13);
            this.buttonCancelStudentsChanges.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancelStudentsChanges.Name = "buttonCancelStudentsChanges";
            this.buttonCancelStudentsChanges.Size = new System.Drawing.Size(130, 30);
            this.buttonCancelStudentsChanges.TabIndex = 3;
            this.buttonCancelStudentsChanges.Text = "Clean changes";
            this.buttonCancelStudentsChanges.UseVisualStyleBackColor = false;
            this.buttonCancelStudentsChanges.Click += new System.EventHandler(this.buttonCancelStudentsChanges_Click);
            // 
            // buttonSaveStudents
            // 
            this.buttonSaveStudents.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonSaveStudents.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSaveStudents.FlatAppearance.BorderSize = 2;
            this.buttonSaveStudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveStudents.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSaveStudents.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveStudents.Location = new System.Drawing.Point(523, 13);
            this.buttonSaveStudents.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveStudents.Name = "buttonSaveStudents";
            this.buttonSaveStudents.Size = new System.Drawing.Size(130, 30);
            this.buttonSaveStudents.TabIndex = 2;
            this.buttonSaveStudents.Text = "Save";
            this.buttonSaveStudents.UseVisualStyleBackColor = false;
            this.buttonSaveStudents.Click += new System.EventHandler(this.buttonSaveStudents_Click);
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.Location = new System.Drawing.Point(16, 59);
            this.dataGridViewStudents.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersWidth = 62;
            this.dataGridViewStudents.RowTemplate.Height = 33;
            this.dataGridViewStudents.Size = new System.Drawing.Size(780, 364);
            this.dataGridViewStudents.TabIndex = 0;
            // 
            // tabPageLessons
            // 
            this.tabPageLessons.Controls.Add(this.buttonCancelLessonsChanges);
            this.tabPageLessons.Controls.Add(this.buttonSaveLessons);
            this.tabPageLessons.Controls.Add(this.dataGridViewLessons);
            this.tabPageLessons.Location = new System.Drawing.Point(4, 24);
            this.tabPageLessons.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageLessons.Name = "tabPageLessons";
            this.tabPageLessons.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageLessons.Size = new System.Drawing.Size(810, 433);
            this.tabPageLessons.TabIndex = 3;
            this.tabPageLessons.Text = "Lessons";
            this.tabPageLessons.UseVisualStyleBackColor = true;
            // 
            // buttonCancelLessonsChanges
            // 
            this.buttonCancelLessonsChanges.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonCancelLessonsChanges.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonCancelLessonsChanges.FlatAppearance.BorderSize = 2;
            this.buttonCancelLessonsChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelLessonsChanges.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancelLessonsChanges.ForeColor = System.Drawing.Color.Black;
            this.buttonCancelLessonsChanges.Location = new System.Drawing.Point(661, 11);
            this.buttonCancelLessonsChanges.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancelLessonsChanges.Name = "buttonCancelLessonsChanges";
            this.buttonCancelLessonsChanges.Size = new System.Drawing.Size(130, 30);
            this.buttonCancelLessonsChanges.TabIndex = 5;
            this.buttonCancelLessonsChanges.Text = "Clean changes";
            this.buttonCancelLessonsChanges.UseVisualStyleBackColor = false;
            this.buttonCancelLessonsChanges.Click += new System.EventHandler(this.buttonCancelLessonsChanges_Click);
            // 
            // buttonSaveLessons
            // 
            this.buttonSaveLessons.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonSaveLessons.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSaveLessons.FlatAppearance.BorderSize = 2;
            this.buttonSaveLessons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveLessons.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSaveLessons.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveLessons.Location = new System.Drawing.Point(518, 11);
            this.buttonSaveLessons.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveLessons.Name = "buttonSaveLessons";
            this.buttonSaveLessons.Size = new System.Drawing.Size(130, 30);
            this.buttonSaveLessons.TabIndex = 4;
            this.buttonSaveLessons.Text = "Save";
            this.buttonSaveLessons.UseVisualStyleBackColor = false;
            this.buttonSaveLessons.Click += new System.EventHandler(this.buttonSaveLessons_Click);
            // 
            // dataGridViewLessons
            // 
            this.dataGridViewLessons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLessons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLessons.Location = new System.Drawing.Point(15, 59);
            this.dataGridViewLessons.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewLessons.Name = "dataGridViewLessons";
            this.dataGridViewLessons.RowHeadersWidth = 62;
            this.dataGridViewLessons.RowTemplate.Height = 33;
            this.dataGridViewLessons.Size = new System.Drawing.Size(780, 364);
            this.dataGridViewLessons.TabIndex = 2;
            // 
            // tabPageAllInvoices
            // 
            this.tabPageAllInvoices.Controls.Add(this.dataGridViewAllInvoices);
            this.tabPageAllInvoices.Location = new System.Drawing.Point(4, 24);
            this.tabPageAllInvoices.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageAllInvoices.Name = "tabPageAllInvoices";
            this.tabPageAllInvoices.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageAllInvoices.Size = new System.Drawing.Size(810, 433);
            this.tabPageAllInvoices.TabIndex = 5;
            this.tabPageAllInvoices.Text = "All Invoices";
            this.tabPageAllInvoices.UseVisualStyleBackColor = true;
            // 
            // dataGridViewAllInvoices
            // 
            this.dataGridViewAllInvoices.AllowUserToAddRows = false;
            this.dataGridViewAllInvoices.AllowUserToDeleteRows = false;
            this.dataGridViewAllInvoices.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewAllInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllInvoices.Location = new System.Drawing.Point(13, 11);
            this.dataGridViewAllInvoices.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewAllInvoices.Name = "dataGridViewAllInvoices";
            this.dataGridViewAllInvoices.ReadOnly = true;
            this.dataGridViewAllInvoices.RowHeadersWidth = 62;
            this.dataGridViewAllInvoices.RowTemplate.Height = 33;
            this.dataGridViewAllInvoices.Size = new System.Drawing.Size(785, 416);
            this.dataGridViewAllInvoices.TabIndex = 6;
            this.dataGridViewAllInvoices.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewAllInvoices_DataBindingComplete);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(846, 479);
            this.Controls.Add(this.tabControlTutorHelper);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Tutor Helper App";
            this.tabControlTutorHelper.ResumeLayout(false);
            this.tabPageInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInvoicesLastMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInvoices)).EndInit();
            this.tabPageLink.ResumeLayout(false);
            this.tabPageLink.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudLessonLink)).EndInit();
            this.tabPageStudents.ResumeLayout(false);
            this.tabPageStudents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.tabPageLessons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLessons)).EndInit();
            this.tabPageAllInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllInvoices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private TabControl tabControlTutorHelper;
        private TabPage tabPageStudents;
        private DataGridView dataGridViewStudents;
        private TabPage tabPageLink;
        private TabPage tabPageLessons;
        private Button buttonSaveStudents;
        private Button buttonCancelStudentsChanges;
        private RadioButton radioButtonShowAll;
        private RadioButton radioButtonShowCurrent;
        private TabPage tabPageInvoices;
        private DataGridView dataGridViewStudLessonLink;
        private RadioButton radioButtonShowAllLink;
        private RadioButton radioButtonShowActual;
        private DataGridView dataGridViewLessons;
        private Button buttonCancelLessonsChanges;
        private Button buttonSaveLessons;
        private RadioButton radioButtonShowHistorical;
        private Button buttonCancelChangesLink;
        private Button buttonSaveLink;
        private DataGridView dataGridViewInvoices;
        private Button buttonCancelChangesInv;
        private Button buttonSendInvoices;
        private DataGridView dataGridViewInvoicesLastMonth;
        private TabPage tabPageAllInvoices;
        private DataGridView dataGridViewAllInvoices;
    }
}