namespace TutorHelper.Forms
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
            tabControlTutorHelper = new TabControl();
            tabPageInvoices = new TabPage();
            dataGridViewInvoicesLastMonth = new DataGridView();
            buttonCancelChangesInv = new Button();
            buttonSendInvoices = new Button();
            dataGridViewInvoices = new DataGridView();
            tabPageLink = new TabPage();
            buttonCancelChangesLink = new Button();
            buttonSaveLink = new Button();
            radioButtonShowHistorical = new RadioButton();
            radioButtonShowAllLink = new RadioButton();
            radioButtonShowActual = new RadioButton();
            dataGridViewStudLessonLink = new DataGridView();
            tabPageStudents = new TabPage();
            radioButtonShowAll = new RadioButton();
            radioButtonShowCurrent = new RadioButton();
            buttonCancelStudentsChanges = new Button();
            buttonSaveStudents = new Button();
            dataGridViewStudents = new DataGridView();
            tabPageLessons = new TabPage();
            buttonCancelLessonsChanges = new Button();
            buttonSaveLessons = new Button();
            dataGridViewLessons = new DataGridView();
            tabPageAllInvoices = new TabPage();
            buttonYearReport = new Button();
            buttonSummaries = new Button();
            radioButtonFromTo = new RadioButton();
            radioButtonYearMonth = new RadioButton();
            dateTimePickerTo = new DateTimePicker();
            dateTimePickerFrom = new DateTimePicker();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            comboBoxMonth = new ComboBox();
            label2 = new Label();
            comboBoxYear = new ComboBox();
            label1 = new Label();
            comboBoxStudent = new ComboBox();
            dataGridViewAllInvoices = new DataGridView();
            tabControlTutorHelper.SuspendLayout();
            tabPageInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInvoicesLastMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInvoices).BeginInit();
            tabPageLink.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudLessonLink).BeginInit();
            tabPageStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudents).BeginInit();
            tabPageLessons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLessons).BeginInit();
            tabPageAllInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAllInvoices).BeginInit();
            SuspendLayout();
            // 
            // tabControlTutorHelper
            // 
            tabControlTutorHelper.Controls.Add(tabPageInvoices);
            tabControlTutorHelper.Controls.Add(tabPageLink);
            tabControlTutorHelper.Controls.Add(tabPageStudents);
            tabControlTutorHelper.Controls.Add(tabPageLessons);
            tabControlTutorHelper.Controls.Add(tabPageAllInvoices);
            tabControlTutorHelper.Location = new Point(21, 13);
            tabControlTutorHelper.Name = "tabControlTutorHelper";
            tabControlTutorHelper.SelectedIndex = 0;
            tabControlTutorHelper.Size = new Size(1468, 768);
            tabControlTutorHelper.TabIndex = 3;
            tabControlTutorHelper.SelectedIndexChanged += tabControlTutorHelper_SelectedIndexChanged;
            // 
            // tabPageInvoices
            // 
            tabPageInvoices.Controls.Add(dataGridViewInvoicesLastMonth);
            tabPageInvoices.Controls.Add(buttonCancelChangesInv);
            tabPageInvoices.Controls.Add(buttonSendInvoices);
            tabPageInvoices.Controls.Add(dataGridViewInvoices);
            tabPageInvoices.Location = new Point(4, 34);
            tabPageInvoices.Name = "tabPageInvoices";
            tabPageInvoices.Size = new Size(1460, 730);
            tabPageInvoices.TabIndex = 4;
            tabPageInvoices.Text = "Invoices";
            tabPageInvoices.UseVisualStyleBackColor = true;
            // 
            // dataGridViewInvoicesLastMonth
            // 
            dataGridViewInvoicesLastMonth.AllowUserToAddRows = false;
            dataGridViewInvoicesLastMonth.AllowUserToDeleteRows = false;
            dataGridViewInvoicesLastMonth.BackgroundColor = Color.AliceBlue;
            dataGridViewInvoicesLastMonth.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInvoicesLastMonth.Location = new Point(20, 490);
            dataGridViewInvoicesLastMonth.Name = "dataGridViewInvoicesLastMonth";
            dataGridViewInvoicesLastMonth.ReadOnly = true;
            dataGridViewInvoicesLastMonth.RowHeadersWidth = 62;
            dataGridViewInvoicesLastMonth.Size = new Size(1419, 222);
            dataGridViewInvoicesLastMonth.TabIndex = 5;
            dataGridViewInvoicesLastMonth.DataBindingComplete += dataGridViewInvoicesLastMonth_DataBindingComplete;
            // 
            // buttonCancelChangesInv
            // 
            buttonCancelChangesInv.BackColor = Color.LightSteelBlue;
            buttonCancelChangesInv.FlatAppearance.BorderColor = Color.Gray;
            buttonCancelChangesInv.FlatAppearance.BorderSize = 2;
            buttonCancelChangesInv.FlatStyle = FlatStyle.Flat;
            buttonCancelChangesInv.Font = new Font("Segoe UI", 10F);
            buttonCancelChangesInv.ForeColor = Color.Black;
            buttonCancelChangesInv.Location = new Point(1253, 24);
            buttonCancelChangesInv.Name = "buttonCancelChangesInv";
            buttonCancelChangesInv.Size = new Size(186, 50);
            buttonCancelChangesInv.TabIndex = 4;
            buttonCancelChangesInv.Text = "Clean changes";
            buttonCancelChangesInv.UseVisualStyleBackColor = false;
            buttonCancelChangesInv.Click += buttonCancelChangesInv_Click;
            // 
            // buttonSendInvoices
            // 
            buttonSendInvoices.BackColor = Color.LightSteelBlue;
            buttonSendInvoices.FlatAppearance.BorderColor = Color.Gray;
            buttonSendInvoices.FlatAppearance.BorderSize = 2;
            buttonSendInvoices.FlatStyle = FlatStyle.Flat;
            buttonSendInvoices.Font = new Font("Segoe UI", 10F);
            buttonSendInvoices.ForeColor = Color.Black;
            buttonSendInvoices.Location = new Point(1048, 24);
            buttonSendInvoices.Name = "buttonSendInvoices";
            buttonSendInvoices.Size = new Size(186, 50);
            buttonSendInvoices.TabIndex = 3;
            buttonSendInvoices.Text = "Send Invoices";
            buttonSendInvoices.UseVisualStyleBackColor = false;
            buttonSendInvoices.Click += buttonSendInvoices_Click;
            // 
            // dataGridViewInvoices
            // 
            dataGridViewInvoices.AllowUserToAddRows = false;
            dataGridViewInvoices.AllowUserToDeleteRows = false;
            dataGridViewInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInvoices.Location = new Point(20, 102);
            dataGridViewInvoices.Name = "dataGridViewInvoices";
            dataGridViewInvoices.RowHeadersWidth = 62;
            dataGridViewInvoices.Size = new Size(1419, 363);
            dataGridViewInvoices.TabIndex = 1;
            dataGridViewInvoices.CellContentClick += dataGridViewInvoices_CellContentClick;
            dataGridViewInvoices.CellValueChanged += dataGridViewInvoices_CellValueChanged;
            dataGridViewInvoices.CurrentCellDirtyStateChanged += dataGridViewInvoices_CurrentCellDirtyStateChanged;
            dataGridViewInvoices.DataBindingComplete += dataGridViewInvoices_DataBindingComplete;
            // 
            // tabPageLink
            // 
            tabPageLink.Controls.Add(buttonCancelChangesLink);
            tabPageLink.Controls.Add(buttonSaveLink);
            tabPageLink.Controls.Add(radioButtonShowHistorical);
            tabPageLink.Controls.Add(radioButtonShowAllLink);
            tabPageLink.Controls.Add(radioButtonShowActual);
            tabPageLink.Controls.Add(dataGridViewStudLessonLink);
            tabPageLink.Location = new Point(4, 34);
            tabPageLink.Name = "tabPageLink";
            tabPageLink.Padding = new Padding(3);
            tabPageLink.Size = new Size(1460, 730);
            tabPageLink.TabIndex = 1;
            tabPageLink.Text = "StudentLessonLink";
            tabPageLink.UseVisualStyleBackColor = true;
            // 
            // buttonCancelChangesLink
            // 
            buttonCancelChangesLink.BackColor = Color.LightSteelBlue;
            buttonCancelChangesLink.FlatAppearance.BorderColor = Color.Gray;
            buttonCancelChangesLink.FlatAppearance.BorderSize = 2;
            buttonCancelChangesLink.FlatStyle = FlatStyle.Flat;
            buttonCancelChangesLink.Font = new Font("Segoe UI", 10F);
            buttonCancelChangesLink.ForeColor = Color.Black;
            buttonCancelChangesLink.Location = new Point(951, 17);
            buttonCancelChangesLink.Name = "buttonCancelChangesLink";
            buttonCancelChangesLink.Size = new Size(186, 50);
            buttonCancelChangesLink.TabIndex = 10;
            buttonCancelChangesLink.Text = "Clean changes";
            buttonCancelChangesLink.UseVisualStyleBackColor = false;
            buttonCancelChangesLink.Click += buttonCancelChangesLink_Click;
            // 
            // buttonSaveLink
            // 
            buttonSaveLink.BackColor = Color.LightSteelBlue;
            buttonSaveLink.FlatAppearance.BorderColor = Color.Gray;
            buttonSaveLink.FlatAppearance.BorderSize = 2;
            buttonSaveLink.FlatStyle = FlatStyle.Flat;
            buttonSaveLink.Font = new Font("Segoe UI", 10F);
            buttonSaveLink.ForeColor = Color.Black;
            buttonSaveLink.Location = new Point(747, 17);
            buttonSaveLink.Name = "buttonSaveLink";
            buttonSaveLink.Size = new Size(186, 50);
            buttonSaveLink.TabIndex = 9;
            buttonSaveLink.Text = "Save";
            buttonSaveLink.UseVisualStyleBackColor = false;
            buttonSaveLink.Click += buttonSaveLink_Click;
            // 
            // radioButtonShowHistorical
            // 
            radioButtonShowHistorical.AutoSize = true;
            radioButtonShowHistorical.Location = new Point(271, 17);
            radioButtonShowHistorical.Name = "radioButtonShowHistorical";
            radioButtonShowHistorical.Size = new Size(159, 29);
            radioButtonShowHistorical.TabIndex = 8;
            radioButtonShowHistorical.TabStop = true;
            radioButtonShowHistorical.Text = "Show Historical";
            radioButtonShowHistorical.UseVisualStyleBackColor = true;
            radioButtonShowHistorical.CheckedChanged += radioButtonShowHistorical_CheckedChanged;
            // 
            // radioButtonShowAllLink
            // 
            radioButtonShowAllLink.AutoSize = true;
            radioButtonShowAllLink.Location = new Point(23, 52);
            radioButtonShowAllLink.Name = "radioButtonShowAllLink";
            radioButtonShowAllLink.Size = new Size(106, 29);
            radioButtonShowAllLink.TabIndex = 7;
            radioButtonShowAllLink.TabStop = true;
            radioButtonShowAllLink.Text = "Show All";
            radioButtonShowAllLink.UseVisualStyleBackColor = true;
            radioButtonShowAllLink.CheckedChanged += radioButtonShowAllLink_CheckedChanged;
            // 
            // radioButtonShowActual
            // 
            radioButtonShowActual.AutoSize = true;
            radioButtonShowActual.Location = new Point(23, 17);
            radioButtonShowActual.Name = "radioButtonShowActual";
            radioButtonShowActual.Size = new Size(177, 29);
            radioButtonShowActual.TabIndex = 6;
            radioButtonShowActual.TabStop = true;
            radioButtonShowActual.Text = "Show Actual Only";
            radioButtonShowActual.UseVisualStyleBackColor = true;
            radioButtonShowActual.CheckedChanged += radioButtonShowActual_CheckedChanged;
            // 
            // dataGridViewStudLessonLink
            // 
            dataGridViewStudLessonLink.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStudLessonLink.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStudLessonLink.Location = new Point(23, 97);
            dataGridViewStudLessonLink.Name = "dataGridViewStudLessonLink";
            dataGridViewStudLessonLink.RowHeadersWidth = 62;
            dataGridViewStudLessonLink.Size = new Size(1114, 607);
            dataGridViewStudLessonLink.TabIndex = 1;
            dataGridViewStudLessonLink.CellContentClick += dataGridViewStudLessonLink_CellContentClick;
            dataGridViewStudLessonLink.CellFormatting += dataGridViewStudLessonLink_CellFormatting;
            dataGridViewStudLessonLink.DataBindingComplete += dataGridViewStudLessonLink_DataBindingComplete;
            // 
            // tabPageStudents
            // 
            tabPageStudents.BackColor = Color.WhiteSmoke;
            tabPageStudents.Controls.Add(radioButtonShowAll);
            tabPageStudents.Controls.Add(radioButtonShowCurrent);
            tabPageStudents.Controls.Add(buttonCancelStudentsChanges);
            tabPageStudents.Controls.Add(buttonSaveStudents);
            tabPageStudents.Controls.Add(dataGridViewStudents);
            tabPageStudents.Location = new Point(4, 34);
            tabPageStudents.Name = "tabPageStudents";
            tabPageStudents.Padding = new Padding(3);
            tabPageStudents.Size = new Size(1460, 730);
            tabPageStudents.TabIndex = 0;
            tabPageStudents.Text = "Students";
            tabPageStudents.Click += tabPageStudents_Click;
            // 
            // radioButtonShowAll
            // 
            radioButtonShowAll.AutoSize = true;
            radioButtonShowAll.Location = new Point(23, 57);
            radioButtonShowAll.Name = "radioButtonShowAll";
            radioButtonShowAll.Size = new Size(106, 29);
            radioButtonShowAll.TabIndex = 5;
            radioButtonShowAll.TabStop = true;
            radioButtonShowAll.Text = "Show All";
            radioButtonShowAll.UseVisualStyleBackColor = true;
            radioButtonShowAll.CheckedChanged += radioButtonShowAll_CheckedChanged;
            // 
            // radioButtonShowCurrent
            // 
            radioButtonShowCurrent.AutoSize = true;
            radioButtonShowCurrent.Location = new Point(23, 22);
            radioButtonShowCurrent.Name = "radioButtonShowCurrent";
            radioButtonShowCurrent.Size = new Size(186, 29);
            radioButtonShowCurrent.TabIndex = 4;
            radioButtonShowCurrent.TabStop = true;
            radioButtonShowCurrent.Text = "Show Current Only";
            radioButtonShowCurrent.UseVisualStyleBackColor = true;
            radioButtonShowCurrent.CheckedChanged += radioButtonShowCurrent_CheckedChanged;
            // 
            // buttonCancelStudentsChanges
            // 
            buttonCancelStudentsChanges.BackColor = Color.LightSteelBlue;
            buttonCancelStudentsChanges.FlatAppearance.BorderColor = Color.Gray;
            buttonCancelStudentsChanges.FlatAppearance.BorderSize = 2;
            buttonCancelStudentsChanges.FlatStyle = FlatStyle.Flat;
            buttonCancelStudentsChanges.Font = new Font("Segoe UI", 10F);
            buttonCancelStudentsChanges.ForeColor = Color.Black;
            buttonCancelStudentsChanges.Location = new Point(951, 22);
            buttonCancelStudentsChanges.Name = "buttonCancelStudentsChanges";
            buttonCancelStudentsChanges.Size = new Size(186, 50);
            buttonCancelStudentsChanges.TabIndex = 3;
            buttonCancelStudentsChanges.Text = "Clean changes";
            buttonCancelStudentsChanges.UseVisualStyleBackColor = false;
            buttonCancelStudentsChanges.Click += buttonCancelStudentsChanges_Click;
            // 
            // buttonSaveStudents
            // 
            buttonSaveStudents.BackColor = Color.LightSteelBlue;
            buttonSaveStudents.FlatAppearance.BorderColor = Color.Gray;
            buttonSaveStudents.FlatAppearance.BorderSize = 2;
            buttonSaveStudents.FlatStyle = FlatStyle.Flat;
            buttonSaveStudents.Font = new Font("Segoe UI", 10F);
            buttonSaveStudents.ForeColor = Color.Black;
            buttonSaveStudents.Location = new Point(747, 22);
            buttonSaveStudents.Name = "buttonSaveStudents";
            buttonSaveStudents.Size = new Size(186, 50);
            buttonSaveStudents.TabIndex = 2;
            buttonSaveStudents.Text = "Save";
            buttonSaveStudents.UseVisualStyleBackColor = false;
            buttonSaveStudents.Click += buttonSaveStudents_Click;
            // 
            // dataGridViewStudents
            // 
            dataGridViewStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStudents.Location = new Point(23, 98);
            dataGridViewStudents.Name = "dataGridViewStudents";
            dataGridViewStudents.RowHeadersWidth = 62;
            dataGridViewStudents.Size = new Size(1114, 607);
            dataGridViewStudents.TabIndex = 0;
            // 
            // tabPageLessons
            // 
            tabPageLessons.Controls.Add(buttonCancelLessonsChanges);
            tabPageLessons.Controls.Add(buttonSaveLessons);
            tabPageLessons.Controls.Add(dataGridViewLessons);
            tabPageLessons.Location = new Point(4, 34);
            tabPageLessons.Name = "tabPageLessons";
            tabPageLessons.Padding = new Padding(3);
            tabPageLessons.Size = new Size(1460, 730);
            tabPageLessons.TabIndex = 3;
            tabPageLessons.Text = "Lessons";
            tabPageLessons.UseVisualStyleBackColor = true;
            // 
            // buttonCancelLessonsChanges
            // 
            buttonCancelLessonsChanges.BackColor = Color.LightSteelBlue;
            buttonCancelLessonsChanges.FlatAppearance.BorderColor = Color.Gray;
            buttonCancelLessonsChanges.FlatAppearance.BorderSize = 2;
            buttonCancelLessonsChanges.FlatStyle = FlatStyle.Flat;
            buttonCancelLessonsChanges.Font = new Font("Segoe UI", 10F);
            buttonCancelLessonsChanges.ForeColor = Color.Black;
            buttonCancelLessonsChanges.Location = new Point(944, 18);
            buttonCancelLessonsChanges.Name = "buttonCancelLessonsChanges";
            buttonCancelLessonsChanges.Size = new Size(186, 50);
            buttonCancelLessonsChanges.TabIndex = 5;
            buttonCancelLessonsChanges.Text = "Clean changes";
            buttonCancelLessonsChanges.UseVisualStyleBackColor = false;
            buttonCancelLessonsChanges.Click += buttonCancelLessonsChanges_Click;
            // 
            // buttonSaveLessons
            // 
            buttonSaveLessons.BackColor = Color.LightSteelBlue;
            buttonSaveLessons.FlatAppearance.BorderColor = Color.Gray;
            buttonSaveLessons.FlatAppearance.BorderSize = 2;
            buttonSaveLessons.FlatStyle = FlatStyle.Flat;
            buttonSaveLessons.Font = new Font("Segoe UI", 10F);
            buttonSaveLessons.ForeColor = Color.Black;
            buttonSaveLessons.Location = new Point(740, 18);
            buttonSaveLessons.Name = "buttonSaveLessons";
            buttonSaveLessons.Size = new Size(186, 50);
            buttonSaveLessons.TabIndex = 4;
            buttonSaveLessons.Text = "Save";
            buttonSaveLessons.UseVisualStyleBackColor = false;
            buttonSaveLessons.Click += buttonSaveLessons_Click;
            // 
            // dataGridViewLessons
            // 
            dataGridViewLessons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLessons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLessons.Location = new Point(21, 98);
            dataGridViewLessons.Name = "dataGridViewLessons";
            dataGridViewLessons.RowHeadersWidth = 62;
            dataGridViewLessons.Size = new Size(1114, 607);
            dataGridViewLessons.TabIndex = 2;
            // 
            // tabPageAllInvoices
            // 
            tabPageAllInvoices.Controls.Add(buttonYearReport);
            tabPageAllInvoices.Controls.Add(buttonSummaries);
            tabPageAllInvoices.Controls.Add(radioButtonFromTo);
            tabPageAllInvoices.Controls.Add(radioButtonYearMonth);
            tabPageAllInvoices.Controls.Add(dateTimePickerTo);
            tabPageAllInvoices.Controls.Add(dateTimePickerFrom);
            tabPageAllInvoices.Controls.Add(label5);
            tabPageAllInvoices.Controls.Add(label4);
            tabPageAllInvoices.Controls.Add(label3);
            tabPageAllInvoices.Controls.Add(comboBoxMonth);
            tabPageAllInvoices.Controls.Add(label2);
            tabPageAllInvoices.Controls.Add(comboBoxYear);
            tabPageAllInvoices.Controls.Add(label1);
            tabPageAllInvoices.Controls.Add(comboBoxStudent);
            tabPageAllInvoices.Controls.Add(dataGridViewAllInvoices);
            tabPageAllInvoices.Location = new Point(4, 34);
            tabPageAllInvoices.Name = "tabPageAllInvoices";
            tabPageAllInvoices.Padding = new Padding(3);
            tabPageAllInvoices.Size = new Size(1460, 730);
            tabPageAllInvoices.TabIndex = 5;
            tabPageAllInvoices.Text = "All Invoices";
            tabPageAllInvoices.UseVisualStyleBackColor = true;
            // 
            // buttonYearReport
            // 
            buttonYearReport.BackColor = Color.LightSteelBlue;
            buttonYearReport.FlatAppearance.BorderColor = Color.Gray;
            buttonYearReport.FlatAppearance.BorderSize = 2;
            buttonYearReport.FlatStyle = FlatStyle.Flat;
            buttonYearReport.Font = new Font("Segoe UI", 10F);
            buttonYearReport.ForeColor = Color.Black;
            buttonYearReport.Location = new Point(946, 70);
            buttonYearReport.Name = "buttonYearReport";
            buttonYearReport.Size = new Size(194, 49);
            buttonYearReport.TabIndex = 20;
            buttonYearReport.Text = "Year Report";
            buttonYearReport.UseVisualStyleBackColor = false;
            buttonYearReport.Click += buttonYearReport_Click;
            // 
            // buttonSummaries
            // 
            buttonSummaries.BackColor = Color.LightSteelBlue;
            buttonSummaries.FlatAppearance.BorderColor = Color.Gray;
            buttonSummaries.FlatAppearance.BorderSize = 2;
            buttonSummaries.FlatStyle = FlatStyle.Flat;
            buttonSummaries.Font = new Font("Segoe UI", 10F);
            buttonSummaries.ForeColor = Color.Black;
            buttonSummaries.Location = new Point(946, 14);
            buttonSummaries.Name = "buttonSummaries";
            buttonSummaries.Size = new Size(194, 49);
            buttonSummaries.TabIndex = 19;
            buttonSummaries.Text = "Prepare Summaries";
            buttonSummaries.UseVisualStyleBackColor = false;
            buttonSummaries.Click += buttonSummaries_Click;
            // 
            // radioButtonFromTo
            // 
            radioButtonFromTo.AutoSize = true;
            radioButtonFromTo.Location = new Point(441, 77);
            radioButtonFromTo.Name = "radioButtonFromTo";
            radioButtonFromTo.Size = new Size(21, 20);
            radioButtonFromTo.TabIndex = 18;
            radioButtonFromTo.TabStop = true;
            radioButtonFromTo.UseVisualStyleBackColor = true;
            radioButtonFromTo.CheckedChanged += radioButtonFromTo_CheckedChanged;
            // 
            // radioButtonYearMonth
            // 
            radioButtonYearMonth.AutoSize = true;
            radioButtonYearMonth.Location = new Point(441, 24);
            radioButtonYearMonth.Name = "radioButtonYearMonth";
            radioButtonYearMonth.Size = new Size(21, 20);
            radioButtonYearMonth.TabIndex = 17;
            radioButtonYearMonth.TabStop = true;
            radioButtonYearMonth.UseVisualStyleBackColor = true;
            radioButtonYearMonth.CheckedChanged += radioButtonYearMonth_CheckedChanged;
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.Format = DateTimePickerFormat.Short;
            dateTimePickerTo.Location = new Point(795, 70);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(135, 31);
            dateTimePickerTo.TabIndex = 16;
            dateTimePickerTo.ValueChanged += dateTimePickerTo_ValueChanged;
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.Format = DateTimePickerFormat.Short;
            dateTimePickerFrom.Location = new Point(549, 70);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(134, 31);
            dateTimePickerFrom.TabIndex = 15;
            dateTimePickerFrom.ValueChanged += dateTimePickerFrom_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(704, 70);
            label5.Name = "label5";
            label5.Size = new Size(30, 25);
            label5.TabIndex = 14;
            label5.Text = "To";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(475, 70);
            label4.Name = "label4";
            label4.Size = new Size(54, 25);
            label4.TabIndex = 13;
            label4.Text = "From";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(704, 14);
            label3.Name = "label3";
            label3.Size = new Size(65, 25);
            label3.TabIndex = 12;
            label3.Text = "Month";
            // 
            // comboBoxMonth
            // 
            comboBoxMonth.FormattingEnabled = true;
            comboBoxMonth.Location = new Point(795, 14);
            comboBoxMonth.Name = "comboBoxMonth";
            comboBoxMonth.Size = new Size(134, 33);
            comboBoxMonth.TabIndex = 11;
            comboBoxMonth.SelectedIndexChanged += comboBoxMonth_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(475, 14);
            label2.Name = "label2";
            label2.Size = new Size(44, 25);
            label2.TabIndex = 10;
            label2.Text = "Year";
            // 
            // comboBoxYear
            // 
            comboBoxYear.FormattingEnabled = true;
            comboBoxYear.Location = new Point(549, 14);
            comboBoxYear.Name = "comboBoxYear";
            comboBoxYear.Size = new Size(134, 33);
            comboBoxYear.TabIndex = 9;
            comboBoxYear.SelectedIndexChanged += comboBoxYear_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 18);
            label1.Name = "label1";
            label1.Size = new Size(73, 25);
            label1.TabIndex = 8;
            label1.Text = "Student";
            // 
            // comboBoxStudent
            // 
            comboBoxStudent.FormattingEnabled = true;
            comboBoxStudent.Location = new Point(114, 15);
            comboBoxStudent.Name = "comboBoxStudent";
            comboBoxStudent.Size = new Size(308, 33);
            comboBoxStudent.TabIndex = 7;
            comboBoxStudent.SelectedIndexChanged += comboBoxStudent_SelectedIndexChanged;
            // 
            // dataGridViewAllInvoices
            // 
            dataGridViewAllInvoices.AllowUserToAddRows = false;
            dataGridViewAllInvoices.AllowUserToDeleteRows = false;
            dataGridViewAllInvoices.BackgroundColor = Color.AliceBlue;
            dataGridViewAllInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAllInvoices.Location = new Point(19, 129);
            dataGridViewAllInvoices.Name = "dataGridViewAllInvoices";
            dataGridViewAllInvoices.ReadOnly = true;
            dataGridViewAllInvoices.RowHeadersWidth = 62;
            dataGridViewAllInvoices.Size = new Size(1121, 582);
            dataGridViewAllInvoices.TabIndex = 6;
            dataGridViewAllInvoices.DataBindingComplete += dataGridViewAllInvoices_DataBindingComplete;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Lavender;
            ClientSize = new Size(1514, 798);
            Controls.Add(tabControlTutorHelper);
            Name = "MainForm";
            Text = "Tutor Helper App";
            tabControlTutorHelper.ResumeLayout(false);
            tabPageInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewInvoicesLastMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInvoices).EndInit();
            tabPageLink.ResumeLayout(false);
            tabPageLink.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudLessonLink).EndInit();
            tabPageStudents.ResumeLayout(false);
            tabPageStudents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudents).EndInit();
            tabPageLessons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewLessons).EndInit();
            tabPageAllInvoices.ResumeLayout(false);
            tabPageAllInvoices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAllInvoices).EndInit();
            ResumeLayout(false);

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
        private Label label1;
        private ComboBox comboBoxStudent;
        private Label label3;
        private ComboBox comboBoxMonth;
        private Label label2;
        private ComboBox comboBoxYear;
        private DateTimePicker dateTimePickerTo;
        private DateTimePicker dateTimePickerFrom;
        private Label label5;
        private Label label4;
        private RadioButton radioButtonFromTo;
        private RadioButton radioButtonYearMonth;
        private Button buttonSummaries;
        private Button buttonYearReport;
    }
}