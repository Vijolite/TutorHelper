using Microsoft.Data.Sqlite;
using System.Data;

namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
        private void LoadDataIntoGridAllInvoices()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand();

            string query;

            if (radioButtonYearMonth.Checked)
            {
                query = @"SELECT s.Name ||' (' || s.Parent || ') - ' || le.Name as Student, 
                            '£' || li.Price || ' ' || li.UsualDay || ' ' || li.UsualTime as Comment, 
                            strftime('%d-%m-%Y', r.LessonDate) as LessonDate, r.LessonTime as LessonTime, strftime('%d-%m-%Y', r.InvoiceDate) as InvoiceDate, 
                            strftime('%d-%m %H:%M', r.InvoiceRecordedDate) as InvoiceRecordedDate,
                            s.Name as StudentName, s.Parent as StudentParent, le.Name as LessonName, li.Price as Price, CAST(s.Id AS TEXT) as StudId
                            FROM StudentLessonLink li INNER JOIN Students s ON s.Id = li.StudentId INNER JOIN Lessons le ON le.Id = li.LessonId
                            INNER JOIN InvoiceRecords r ON li.Id = r.LinkId
                            WHERE (@StudentId = -1 OR s.Id = @StudentId)
                            AND (@Year = '<All>' OR strftime('%Y', r.LessonDate) = @Year)
                            AND (@Month = 0 OR CAST(strftime('%m', r.LessonDate) AS INTEGER) = @Month)
                            ORDER BY r.InvoiceRecordedDate DESC";

                command.Parameters.AddWithValue("@Year", filterYear);
                command.Parameters.AddWithValue("@Month", filterMonth);
            }
            else
            {
                string dateFrom = dateTimePickerFrom.Value.ToString("yyyy-MM-dd");
                string dateTo = dateTimePickerTo.Value.ToString("yyyy-MM-dd");

                query = @"SELECT s.Name ||' (' || s.Parent || ') - ' || le.Name as Student, 
                            '£' || li.Price || ' ' || li.UsualDay || ' ' || li.UsualTime as Comment, 
                            strftime('%d-%m-%Y', r.LessonDate) as LessonDate, r.LessonTime as LessonTime, strftime('%d-%m-%Y', r.InvoiceDate) as InvoiceDate, 
                            strftime('%d-%m %H:%M', r.InvoiceRecordedDate) as InvoiceRecordedDate,
                            s.Name as StudentName, s.Parent as StudentParent, le.Name as LessonName, li.Price as Price, CAST(s.Id AS TEXT) as StudId
                            FROM StudentLessonLink li INNER JOIN Students s ON s.Id = li.StudentId INNER JOIN Lessons le ON le.Id = li.LessonId
                            INNER JOIN InvoiceRecords r ON li.Id = r.LinkId
                            WHERE (@StudentId = -1 OR s.Id = @StudentId)
                            AND DATE(r.LessonDate) BETWEEN @From AND @To
                            ORDER BY r.InvoiceRecordedDate DESC";

                command.Parameters.AddWithValue("@From", dateFrom);
                command.Parameters.AddWithValue("@To", dateTo);
            }

            command.Parameters.AddWithValue("@StudentId", filterStudentId);

            command.CommandText = query;
            command.Connection = connection;

            using var reader = command.ExecuteReader();

            tableAllInvoices = new DataTable();
            tableAllInvoices.Load(reader);  // Load data from reader into DataTable

            dataGridViewAllInvoices.AutoGenerateColumns = true;
            dataGridViewAllInvoices.Columns.Clear();

            dataGridViewAllInvoices.DataSource = tableAllInvoices;

            dataGridViewAllInvoices.Columns["Student"].HeaderText = "Student - Lesson";
            dataGridViewAllInvoices.Columns["Comment"].HeaderText = "More Details";
            dataGridViewAllInvoices.Columns["LessonDate"].HeaderText = "Lesson Date";
            dataGridViewAllInvoices.Columns["LessonTime"].HeaderText = "Lesson Time";
            dataGridViewAllInvoices.Columns["InvoiceDate"].HeaderText = "Invoice Date";
            dataGridViewAllInvoices.Columns["InvoiceRecordedDate"].HeaderText = "Recorded At";

            dataGridViewAllInvoices.DataBindingComplete += dataGridViewAllInvoices_DataBindingComplete;
        }

        private void dataGridViewAllInvoices_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewAllInvoices.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridViewAllInvoices.Columns["Student"].Width = 300;
            dataGridViewAllInvoices.Columns["Comment"].Width = 170;
            dataGridViewAllInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewAllInvoices.Columns["StudentName"].Visible = false;
            dataGridViewAllInvoices.Columns["StudentParent"].Visible = false;
            dataGridViewAllInvoices.Columns["StudId"].Visible = false;
            dataGridViewAllInvoices.Columns["LessonName"].Visible = false;
            dataGridViewAllInvoices.Columns["Price"].Visible = false;
        }

        private void PrepareSummaryAndBalanceFiles()
        {
            var rowsFilteredForSummaries = tableAllInvoices.AsEnumerable()
                .ToList();
            var rowsGroupedByStudent = rowsFilteredForSummaries.GroupBy(row => row["StudId"]);

            string filteredStudents = string.Join(Environment.NewLine, rowsFilteredForSummaries.Select(row => "- " + row["Student"]).Distinct());

            if (filteredStudents.Length == 0)
            {
                MessageBox.Show("No lines are filtered for summaries preparation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radioButtonYearMonth.Checked && (filterYear == "<All>" || filterMonth == 0))
            {
                MessageBox.Show("Year and month should be specified, alternatively you should select from...to... dates", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                        $"Do you want to prepare summaries for these students? {Environment.NewLine}{filteredStudents}", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                var changesPerformed = false;
                foreach (var group in rowsGroupedByStudent)
                {
                    var id = group.Key;
                    //MessageBox.Show(id.ToString());
                    var studentData = group.First();
                    string studentName = studentData["StudentName"].ToString();
                    string studentParent = studentData["StudentParent"].ToString();
                    string lessonName = studentData["LessonName"].ToString();
                    string price = $"£{studentData["Price"].ToString()}";
                    string summaryDate = DateTime.Now.ToString("dd/MM/yyyy");

                    var lessonDates = group.Select(row => ToDateFormat(row["LessonDate"], "dd-mm-yyyy", "dd/mm/yyyy")).OrderBy(d => ToDateFormat(d, "dd/mm/yyyy", "yyyymmdd"));

                    var replacements = new Dictionary<string, string>
                                            {
                                                { "{studentName}", studentName },
                                                { "{parentName}", studentParent },
                                                { "{lessonName}", lessonName },
                                                { "{summaryDate}", summaryDate },
                                            };
                    int ind = 1;
                    foreach (var lessonDate in lessonDates)
                    {
                        replacements.Add($"{{lessonDate{ind}}}", lessonDate);
                        replacements.Add($"{{price{ind}}}", price);
                        ind++;
                    }
                    for (int i = ind; i <= 6; i++)
                    {
                        replacements.Add($"{{lessonDate{i}}}", "");
                        replacements.Add($"{{price{i}}}", "");
                    }

                    string templateFile = templatePath + "summaryTemplate.docx";

                    string outputFileName = radioButtonYearMonth.Checked ? $"end_of_month_summary_{studentName}_{comboBoxMonth.SelectedItem.ToString()}{filterYear}" :
                        $"end_of_month_summary_{studentName}_{dateTimePickerFrom.Value.ToString("dd-MM-yyyy")}_{dateTimePickerTo.Value.ToString("dd-MM-yyyy")}";

                    string outputPath = connectionString == "Data Source=tutorhelper.db" ? @$"{invoicesPath}Invoices\{outputFileName}.docx" : @$"{invoicesPath}InvoicesTest\{outputFileName}.docx";

                    WordTemplateHelper.ReplacePlaceholders(templateFile, outputPath, replacements);
                }
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Summaries are prepared and saved", "Action Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonSummaries_Click(object sender, EventArgs e)
        {
            PrepareSummaryAndBalanceFiles();
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridAllInvoices();
        }

        // =================
        // Combo Box
        // =================

        private void SetupComboBox(ComboBox comboBox, Func<DataTable> getValues)
        {
            // Create and insert the "<All>" row
            DataTable dataTable = getValues();
            DataRow emptyRow = dataTable.NewRow();
            emptyRow["Id"] = -1;           // Use a special value for "All"
            emptyRow["Name"] = "<All>";
            dataTable.Rows.InsertAt(emptyRow, 0);  // Insert at the top

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = "Name";  // what user sees
            comboBox.ValueMember = "Id";      // underlying value (ID)
        }

        private void SetupComboBoxWithOneColumn(ComboBox comboBox, Func<DataTable> getValues)
        {
            // Create and insert the "<All>" row
            DataTable dataTable = getValues();
            DataRow emptyRow = dataTable.NewRow();
            emptyRow["Name"] = "<All>";
            dataTable.Rows.InsertAt(emptyRow, 0);  // Insert at the top

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = "Name";  // what user sees
            comboBox.ValueMember = "Name";
        }

        private void SetupComboBoxForMonths()
        {
            comboBoxMonth.Items.Clear();
            comboBoxMonth.Items.Add("<All>");  // index 0

            var monthNames = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames;

            foreach (var month in monthNames)
            {
                if (!string.IsNullOrWhiteSpace(month))  // skip the 13th empty entry
                    comboBoxMonth.Items.Add(month);
            }

            comboBoxMonth.SelectedIndex = 0;
        }

        private void comboBoxStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudent.SelectedValue == null || comboBoxStudent.SelectedValue is DataRowView)
                return;

            filterStudentId = Convert.ToInt64(comboBoxStudent.SelectedValue);

            // Now reload grid 
            LoadDataIntoGridAllInvoices();
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxYear.SelectedValue == null || comboBoxYear.SelectedValue is DataRowView)
                return;
            filterYear = comboBoxYear.SelectedValue.ToString();

            // Now reload grid 
            LoadDataIntoGridAllInvoices();
        }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterMonth = comboBoxMonth.SelectedIndex;

            // Now reload grid 
            LoadDataIntoGridAllInvoices();
        }

        private void radioButtonYearMonth_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerFrom.Enabled = false;
            dateTimePickerTo.Enabled = false;
            comboBoxMonth.Enabled = true;
            comboBoxYear.Enabled = true;
            LoadDataIntoGridAllInvoices();
        }

        private void radioButtonFromTo_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerFrom.Enabled = true;
            dateTimePickerTo.Enabled = true;
            comboBoxMonth.Enabled = false;
            comboBoxYear.Enabled = false;
            comboBoxMonth.SelectedIndex = 0;
            comboBoxYear.SelectedIndex = 0;
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridAllInvoices();
        }
    }
}
