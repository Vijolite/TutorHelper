using TutorHelper.DataBaseConnection;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Color = System.Drawing.Color;
using DocumentFormat.OpenXml;
using TutorHelper.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using Font = System.Drawing.Font;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.VariantTypes;
using static System.Windows.Forms.LinkLabel;
using System.Security.Cryptography.Xml;
using DocumentFormat.OpenXml.Wordprocessing;
using TutorHelper.Helpers;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;
using ComboBox = System.Windows.Forms.ComboBox;
using DocumentFormat.OpenXml.Bibliography;

namespace TutorHelper
{
    public partial class MainForm : Form
    {
        string connectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
        string templatePath = ConfigurationManager.AppSettings["InvoiceTemplatePath"];
        string invoicesPath = ConfigurationManager.AppSettings["PreparedInvoicesPath"];

        private DataTable tableStudents;
        private DataTable tableLessons;
        private DataTable tableStudentLessonLink;
        private DataTable tableInvoices;
        private DataTable tableInvoicesLastMonth;
        private DataTable tableAllInvoices;

        private long filterStudentId = -1;
        private string filterYear = "<All>";
        private int filterMonth = 0;
        public MainForm()
        {
            InitializeComponent();
            this.BackColor = Color.Lavender;
            //Page StudLessonLink
            Generic_SetupTabStylesAndLoadGrid(tabPageLink, dataGridViewStudLessonLink, LoadDataIntoGridStudLessonLink);
            radioButtonShowActual.Checked = true;
            radioButtonShowAllLink.Checked = false;
            //Page Students
            Generic_SetupTabStylesAndLoadGrid(tabPageStudents, dataGridViewStudents, LoadDataIntoGridStudents);
            radioButtonShowCurrent.Checked = true;
            radioButtonShowAll.Checked = false;
            //Page Lessons
            Generic_SetupTabStylesAndLoadGrid(tabPageLessons, dataGridViewLessons, LoadDataIntoGridLessons);
            //Page Invoices
            Generic_SetupTabStylesAndLoadGrid(tabPageInvoices, dataGridViewInvoices, LoadDataIntoGridInvoices);
            Generic_SetupTabStylesAndLoadGrid(tabPageInvoices, dataGridViewInvoicesLastMonth, LoadDataIntoGridInvoicesLastMonth);
            //All Invoices
            Generic_SetupTabStylesAndLoadGrid(tabPageAllInvoices, dataGridViewAllInvoices, LoadDataIntoGridAllInvoices);

            //Add generic methods
            dataGridViewStudents.UserDeletingRow += Generic_UserDeletingRow;
            dataGridViewLessons.UserDeletingRow += Generic_UserDeletingRow;
            dataGridViewStudLessonLink.UserDeletingRow += Generic_UserDeletingRow;
            dataGridViewStudents.RowPrePaint += Generic_RowPrePaint;
            dataGridViewLessons.RowPrePaint += Generic_RowPrePaint;
            dataGridViewStudLessonLink.RowPrePaint += Generic_RowPrePaint;
        }

        private void tabControlTutorHelper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlTutorHelper.SelectedTab == tabPageStudents)
            {
                LoadDataIntoGridStudents();
            }
            if (tabControlTutorHelper.SelectedTab == tabPageLink)
            {
                radioButtonShowActual.Checked = true;
                LoadDataIntoGridStudLessonLink();
            }
            if (tabControlTutorHelper.SelectedTab == tabPageInvoices)
            {
                LoadDataIntoGridInvoices();
                LoadDataIntoGridInvoicesLastMonth();
            }
            if (tabControlTutorHelper.SelectedTab == tabPageLessons)
            {
                LoadDataIntoGridLessons();
            }
            if (tabControlTutorHelper.SelectedTab == tabPageAllInvoices)
            {
                radioButtonYearMonth.Checked = true;
                SetupComboBox(comboBoxStudent, GetAllStudents);
                SetupComboBoxWithOneColumn(comboBoxYear, GetAllYears);
                SetupComboBoxForMonths();
                LoadDataIntoGridAllInvoices();
            }
        }

        // =================
        // Students
        // =================
        private void LoadDataIntoGridStudents()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string query = radioButtonShowAll.Checked ? "SELECT * FROM Students" : "SELECT * FROM Students WHERE Current=TRUE";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            tableStudents = new DataTable();
            tableStudents.Load(reader);  // Load data from reader into DataTable

            tableStudents.Columns.Add("IsMarkedDeleted", typeof(bool));

            AllowDBNullToDataTable(tableStudents);

            dataGridViewStudents.DataSource = tableStudents;

            dataGridViewStudents.Columns["Name"].HeaderText = "Student's Name";
            dataGridViewStudents.Columns["Parent"].HeaderText = "Parents's Name";
            dataGridViewStudents.Columns["Email"].HeaderText = "Parent's Email";
            dataGridViewStudents.Columns["Id"].Visible = false;
            dataGridViewStudents.Columns["IsMarkedDeleted"].Visible = false;

            ReplaceColumnWithCheckBoxColumn(dataGridViewStudents, "Current");

            dataGridViewStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonSaveStudents_Click(object sender, EventArgs e)
        {
            SaveChangesStudents();
        }

        private void SaveChangesStudents()
        {
            DialogResult result = MessageBox.Show(
        "Do you really want to save changes?",
        "Confirm Save",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                bool changesPerformed = false;

                foreach (DataRow row in tableStudents.Rows)
                {
                    // Skip rows that are also marked deleted (as we're using soft-delete flag)
                    if (row["IsMarkedDeleted"] is bool isDeleted && isDeleted)
                        continue;

                    string errorMessage;
                    if (!ValidationPassedDataRow(row, new List<string> { "Name", "Parent", "Email" }, new List<string> { }, new List<string> { }, out errorMessage))
                    {
                        MessageBox.Show($"Errors in line {DataRowToString(row, new[] { "Name", "Parent", "Email" })}{Environment.NewLine}{errorMessage}",
                                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (row.RowState == DataRowState.Modified)
                    {
                        bool isTryingToDeactivate = row["Current"] == DBNull.Value;
                        int studentId = Convert.ToInt32(row["Id"]);
                        if (isTryingToDeactivate && !CanDeactivateStudent(studentId))
                        {
                            MessageBox.Show($"Cannot deactivate student '{row["Name"]}' with active links to lessons",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue; // Skip this update
                        }

                        string updateSql = "UPDATE Students SET Name = $name, Parent = $parent, Email = $email, Current = $current WHERE Id = $id";

                        using var cmd = new SqliteCommand(updateSql, connection);
                        cmd.Parameters.AddWithValue("$name", row["Name"]);
                        cmd.Parameters.AddWithValue("$parent", row["Parent"]);
                        cmd.Parameters.AddWithValue("$email", row["Email"]);
                        cmd.Parameters.AddWithValue("$current", row["Current"]);
                        cmd.Parameters.AddWithValue("$id", row["Id"]);

                        cmd.ExecuteNonQuery();
                        changesPerformed = true;
                    }
                    else if (row.RowState == DataRowState.Added)
                    {
                        string insertSql = "INSERT INTO Students (Name, Parent, Email, Current) VALUES ($name, $parent, $email, $current)";

                        using var cmd = new SqliteCommand(insertSql, connection);
                        cmd.Parameters.AddWithValue("$name", row["Name"]);
                        cmd.Parameters.AddWithValue("$parent", row["Parent"]);
                        cmd.Parameters.AddWithValue("$email", row["Email"]);
                        cmd.Parameters.AddWithValue("$current", row["Current"]);

                        cmd.ExecuteNonQuery();
                        changesPerformed = true;
                    };
                }
                // Step 1: Prepare a list of rows to process
                var rowsToDelete = tableStudents.AsEnumerable()
                    .Where(row => row.RowState != DataRowState.Added && // Skip newly added rows
                    row["IsMarkedDeleted"] is bool isDeleted && isDeleted)
                    .ToList();

                // Step 2: Process each row safely
                foreach (var row in rowsToDelete)
                {
                    var id = row["Id", DataRowVersion.Original];
                    int studentId = Convert.ToInt32(id);

                    // Check if student can be deleted
                    if (!CanDeleteStudent(studentId))
                    {
                        MessageBox.Show($"Cannot delete student '{row["Name", DataRowVersion.Original]}' as they have links to lessons.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    // Ensure student is not marked as Current
                    if (row["Current", DataRowVersion.Original] != DBNull.Value)
                    {
                        MessageBox.Show($"Cannot delete a current student '{row["Name", DataRowVersion.Original]}'. Please deactivate them first.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    // Step 3: Execute deletion
                    string deleteSql = "DELETE FROM Students WHERE Id = $id";

                    using var cmd = new SqliteCommand(deleteSql, connection);
                    cmd.Parameters.AddWithValue("$id", id);
                    cmd.ExecuteNonQuery();

                    changesPerformed = true;
                }

                if (changesPerformed)
                {
                    tableStudents.AcceptChanges();
                    MessageBox.Show("Changes have been saved successfully.", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDataIntoGridStudents();
            }
        }

        private bool CanDeactivateStudent(int studentId)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT COUNT(*) FROM StudentLessonLink 
                WHERE StudentId = $studentId AND Actual = 1;
                ";
            command.Parameters.AddWithValue("$studentId", studentId);

            long count = (long)command.ExecuteScalar();

            return count == 0; // True if no active lessons
        }

        private bool CanDeleteStudent(int studentId)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT COUNT(*) FROM StudentLessonLink 
                WHERE StudentId = $studentId;
                ";
            command.Parameters.AddWithValue("$studentId", studentId);

            long count = (long)command.ExecuteScalar();

            return count == 0; // True if no lessons linked to the student
        }

        private void buttonCancelStudentsChanges_Click(object sender, EventArgs e)
        {
            LoadDataIntoGridStudents();
        }

        private void tabPageStudents_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonShowAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridStudents();
        }

        private void radioButtonShowCurrent_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridStudents();
        }


        // =================
        // StudentLessonLink
        // =================
        private void LoadDataIntoGridStudLessonLink()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string query;
            if (radioButtonShowAllLink.Checked)
                query =
            @"SELECT li.Id as Id, li.StudentId as StudentId, li.LessonId as LessonId,
                li.UsualDay as Day, li.UsualTime as Time, li.Price as Price, li.Actual as Actual
                FROM StudentLessonLink li INNER JOIN Students s ON s.Id = li.StudentId INNER JOIN Lessons le ON le.Id = li.LessonId
                WHERE s.Current=true
                ORDER BY s.Name";
            else if (radioButtonShowActual.Checked)
                query =
                @"SELECT li.Id as Id, li.StudentId as StudentId, li.LessonId as LessonId,
                li.UsualDay as Day, li.UsualTime as Time, li.Price as Price, li.Actual as Actual
                FROM StudentLessonLink li INNER JOIN Students s ON s.Id = li.StudentId INNER JOIN Lessons le ON le.Id = li.LessonId
                WHERE s.Current=true AND li.Actual=true
                ORDER BY s.Name";
            else //if (radioButtonShowHistorical.Checked)
                query =
                @"SELECT li.Id as Id, li.StudentId as StudentId, li.LessonId as LessonId,
                li.UsualDay as Day, li.UsualTime as Time, li.Price as Price, li.Actual as Actual
                FROM StudentLessonLink li INNER JOIN Students s ON s.Id = li.StudentId INNER JOIN Lessons le ON le.Id = li.LessonId
                ORDER BY s.Name";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            tableStudentLessonLink = new DataTable();
            tableStudentLessonLink.Load(reader);  // Load data from reader into DataTable
            tableStudentLessonLink.Columns.Add("IsMarkedDeleted", typeof(bool));

            AllowDBNullToDataTable(tableStudentLessonLink);

            dataGridViewStudLessonLink.DataSource = tableStudentLessonLink;

            //Add combo box column for Students
            if (radioButtonShowHistorical.Checked)
            {
                SetupComboBoxColumn(dataGridViewStudLessonLink, "StudentNameCombo", 3, "Student", "StudentId", GetAllStudents);
            }
            else
            {
                SetupComboBoxColumn(dataGridViewStudLessonLink, "StudentNameCombo", 3, "Student", "StudentId", GetCurrentStudents);
            }

            //Add combo box column for Lessons
            SetupComboBoxColumn(dataGridViewStudLessonLink, "LessonNameCombo", 4, "Lesson", "LessonId", GetLessons);

            dataGridViewStudLessonLink.Columns["Day"].HeaderText = "Usual Day";
            dataGridViewStudLessonLink.Columns["Time"].HeaderText = "Usual Time";
            dataGridViewStudLessonLink.Columns["Price"].HeaderText = "Price £";
            dataGridViewStudLessonLink.Columns["Actual"].HeaderText = "Actual";

            dataGridViewStudLessonLink.Columns["Id"].Visible = false;
            dataGridViewStudLessonLink.Columns["StudentId"].Visible = false;
            dataGridViewStudLessonLink.Columns["LessonId"].Visible = false;
            dataGridViewStudLessonLink.Columns["IsMarkedDeleted"].Visible = false;

            ReplaceColumnWithCheckBoxColumn(dataGridViewStudLessonLink, "Actual");

            dataGridViewStudLessonLink.DataBindingComplete += dataGridViewStudLessonLink_DataBindingComplete;
        }

        private void radioButtonShowActual_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridStudLessonLink();
            buttonSaveLink.Enabled = true;
            dataGridViewStudLessonLink.ReadOnly = false;
            dataGridViewStudLessonLink.DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void radioButtonShowAllLink_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridStudLessonLink();
            buttonSaveLink.Enabled = true;
            dataGridViewStudLessonLink.ReadOnly = false;
            dataGridViewStudLessonLink.DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void radioButtonShowHistorical_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridStudLessonLink();
            buttonSaveLink.Enabled = false;
            dataGridViewStudLessonLink.ReadOnly = true;
            dataGridViewStudLessonLink.DefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void buttonCancelChangesLink_Click(object sender, EventArgs e)
        {
            LoadDataIntoGridStudLessonLink();
        }

        private void buttonSaveLink_Click(object sender, EventArgs e)
        {
            SaveChangesStudentLessonLink();
        }

        private void SaveChangesStudentLessonLink()
        {
            DialogResult result = MessageBox.Show(
        "Do you really want to save changes?",
        "Confirm Save",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                bool changesPerformed = false;

                foreach (DataRow row in tableStudentLessonLink.Rows)
                {
                    // Skip rows that are also marked deleted (as we're using soft-delete flag)
                    if (row["IsMarkedDeleted"] is bool isDeleted && isDeleted)
                        continue;

                    string errorMessage;
                    if (!ValidationPassedDataRow(row, new List<string> { "StudentId", "LessonId", "Day", "Time", "Price" }, new List<string> { }, new List<string> { "Price" }, out errorMessage))
                    {
                        int studentId = (row["StudentId"] == DBNull.Value ? 0 : Convert.ToInt32(row["StudentId"]));
                        string studentName = FindNameByIdInComboBox("StudentNameCombo", studentId);
                        int lessonId = (row["LessonId"] == DBNull.Value ? 0 : Convert.ToInt32(row["LessonId"]));
                        string lesson = FindNameByIdInComboBox("LessonNameCombo", lessonId);
                        MessageBox.Show($"Errors in line {studentName} {lesson} {DataRowToString(row, new[] { "Day", "Time", "Price" })}{Environment.NewLine}{errorMessage}",
                                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (row.RowState == DataRowState.Modified)
                    {
                        string updateSql = @"UPDATE StudentLessonLink SET StudentId = $studentId, LessonId = $lessonId, Price = $price, 
                                            UsualDay = $usualDay, UsualTime = $usualTime, Actual = $actual WHERE Id = $id";

                        using var cmd = new SqliteCommand(updateSql, connection);
                        cmd.Parameters.AddWithValue("$studentId", row["StudentId"]);
                        cmd.Parameters.AddWithValue("$lessonId", row["LessonId"]);
                        cmd.Parameters.AddWithValue("$price", row["Price"]);
                        cmd.Parameters.AddWithValue("$usualDay", row["Day"]);
                        cmd.Parameters.AddWithValue("$usualTime", row["Time"]);
                        cmd.Parameters.AddWithValue("$actual", row["Actual"]);
                        cmd.Parameters.AddWithValue("$id", row["Id"]);

                        cmd.ExecuteNonQuery();
                        changesPerformed = true;
                    }
                    else if (row.RowState == DataRowState.Added)
                    {
                        string insertSql = @"INSERT INTO StudentLessonLink (StudentId, LessonId, Price, UsualDay, UsualTime, Actual)
                                            VALUES ($studentId, $lessonId, $price, $usualDay, $usualTime, $actual)";

                        using var cmd = new SqliteCommand(insertSql, connection);
                        cmd.Parameters.AddWithValue("$studentId", row["StudentId"]);
                        cmd.Parameters.AddWithValue("$lessonId", row["LessonId"]);
                        cmd.Parameters.AddWithValue("$price", row["Price"]);
                        cmd.Parameters.AddWithValue("$usualDay", row["Day"]);
                        cmd.Parameters.AddWithValue("$usualTime", row["Time"]);
                        cmd.Parameters.AddWithValue("$actual", row["Actual"]);

                        cmd.ExecuteNonQuery();
                        changesPerformed = true;
                    };
                }
                // Step 1: Prepare a list of rows to process
                var rowsToDelete = tableStudentLessonLink.AsEnumerable()
                    .Where(row => row.RowState != DataRowState.Added && // Skip newly added rows
                    row["IsMarkedDeleted"] is bool isDeleted && isDeleted)
                    .ToList();

                // Step 2: Process each row safely
                foreach (var row in rowsToDelete)
                {
                    var id = row["Id", DataRowVersion.Original];
                    int linkId = Convert.ToInt32(id);

                    // Ensure link is not marked as Actual
                    if (row["Actual", DataRowVersion.Original] != DBNull.Value)
                    {
                        int studentId = Convert.ToInt32(row["StudentId", DataRowVersion.Original]);
                        string studentName = FindNameByIdInComboBox("StudentNameCombo", studentId);
                        int lessonId = Convert.ToInt32(row["LessonId", DataRowVersion.Original]);
                        string lesson = FindNameByIdInComboBox("LessonNameCombo", lessonId);

                        MessageBox.Show($"Cannot delete an actual link for {studentName}-{lesson}. Please deactivate it first.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    // Step 3: Execute deletion
                    string deleteSql = "DELETE FROM StudentLessonLink WHERE Id = $id";

                    using var cmd = new SqliteCommand(deleteSql, connection);
                    cmd.Parameters.AddWithValue("$id", id);
                    cmd.ExecuteNonQuery();

                    changesPerformed = true;
                }

                if (changesPerformed)
                {
                    tableStudentLessonLink.AcceptChanges();
                    MessageBox.Show("Changes have been saved successfully.", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDataIntoGridStudLessonLink();
            }
        }

        private void dataGridViewStudLessonLink_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewStudLessonLink.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // =================
        // Combo Box Column
        // =================

        private void SetupComboBoxColumn(DataGridView grid, string comboColumnName, int insertAtColumnIndex, string headerText,
            string dataPropertyName, Func<DataTable> getValues)
        {

            if (!dataGridViewStudLessonLink.Columns.Contains(comboColumnName))
            {
                var comboColumn = new DataGridViewComboBoxColumn();
                comboColumn.Name = comboColumnName;
                comboColumn.HeaderText = headerText;

                // The grid’s data source has a column "StudentId" which will be saved in this column
                comboColumn.DataPropertyName = dataPropertyName;

                // Data source for dropdown to show actual names
                comboColumn.DataSource = getValues();// e.g. DataTable or List<Student>
                comboColumn.DisplayMember = "Name";  // The name shown in dropdown
                comboColumn.ValueMember = "Id";      // The actual value saved in DataGridView

                comboColumn.Width = 250;
                comboColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                grid.Columns.Insert(insertAtColumnIndex, comboColumn);
            }
            else // refresh content if column exists
            {
                var comboColumn = dataGridViewStudLessonLink.Columns[comboColumnName] as DataGridViewComboBoxColumn;
                if (comboColumn != null)
                {
                    comboColumn.DataSource = getValues();
                }
            }
        }

        private string FindNameByIdInComboBox(string comboBoxColumnName, int id)
        {
            // Find the name in the combo box data source
            var dataTable = (DataTable)((DataGridViewComboBoxColumn)dataGridViewStudLessonLink.Columns[comboBoxColumnName]).DataSource;
            var dataRow = dataTable.Rows.Cast<DataRow>().FirstOrDefault(r => Convert.ToInt32(r["Id"]) == id);

            return dataRow?["Name"].ToString() ?? "(unknown)";
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



        // =================
        // Check Box Column
        // =================

        private void ReplaceColumnWithCheckBoxColumn(DataGridView grid, string columnName)
        {
            // Replace Current column with checkbox column
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.DataPropertyName = columnName; // Bind to this column in your DataTable
            checkBoxColumn.HeaderText = columnName;
            checkBoxColumn.Name = columnName;
            // Optional: remove existing column if auto-generated
            if (grid.Columns[columnName] != null)
            {
                grid.Columns.Remove(columnName);
            }
            // Add the checkbox column
            grid.Columns.Add(checkBoxColumn);
        }

        // =================
        // Lessons
        // =================

        private void LoadDataIntoGridLessons()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM Lessons";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            tableLessons = new DataTable();
            tableLessons.Load(reader);  // Load data from reader into DataTable

            tableLessons.Columns.Add("IsMarkedDeleted", typeof(bool));

            AllowDBNullToDataTable(tableLessons);

            dataGridViewLessons.DataSource = tableLessons;

            dataGridViewLessons.Columns["Name"].HeaderText = "Lesson";
            dataGridViewLessons.Columns["Id"].Visible = false;
            dataGridViewLessons.Columns["IsMarkedDeleted"].Visible = false;

            dataGridViewLessons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonCancelLessonsChanges_Click(object sender, EventArgs e)
        {
            LoadDataIntoGridLessons();
        }

        private void buttonSaveLessons_Click(object sender, EventArgs e)
        {
            SaveChangesLessons();
        }

        private void SaveChangesLessons()
        {
            DialogResult result = MessageBox.Show(
        "Do you really want to save changes?",
        "Confirm Save",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                bool changesPerformed = false;

                foreach (DataRow row in tableLessons.Rows)
                {
                    // Skip rows that are also marked deleted (as we're using soft-delete flag)
                    if (row["IsMarkedDeleted"] is bool isDeleted && isDeleted)
                        continue;

                    string errorMessage;
                    if (!ValidationPassedDataRow(row, new List<string> { "Name" }, new List<string> { }, new List<string> { }, out errorMessage))
                    {
                        MessageBox.Show($"Errors in line {DataRowToString(row, new[] { "Name" })}{Environment.NewLine}{errorMessage}",
                                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (row.RowState == DataRowState.Modified)
                    {
                        string updateSql = "UPDATE Lessons SET Name = $name WHERE Id = $id";

                        using var cmd = new SqliteCommand(updateSql, connection);
                        cmd.Parameters.AddWithValue("$name", row["Name"]);
                        cmd.Parameters.AddWithValue("$id", row["Id"]);

                        cmd.ExecuteNonQuery();
                        changesPerformed = true;
                    }
                    else if (row.RowState == DataRowState.Added)
                    {
                        string insertSql = "INSERT INTO Lessons (Name) VALUES ($name)";

                        using var cmd = new SqliteCommand(insertSql, connection);
                        cmd.Parameters.AddWithValue("$name", row["Name"]);

                        cmd.ExecuteNonQuery();
                        changesPerformed = true;
                    };
                }
                // Step 1: Prepare a list of rows to process
                var rowsToDelete = tableLessons.AsEnumerable()
                    .Where(row =>
                        row.RowState != DataRowState.Added && // Skip newly added rows
                        row["IsMarkedDeleted"] is bool isDeleted && isDeleted)
                    .ToList();

                // Step 2: Process each row safely
                foreach (var row in rowsToDelete)
                {
                    var id = row["Id", DataRowVersion.Original];
                    int lessonId = Convert.ToInt32(id);

                    // Check if student can be deleted
                    if (!CanDeleteLesson(lessonId))
                    {
                        MessageBox.Show($"Cannot delete lesson '{row["Name", DataRowVersion.Original]}' as they have links to students.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    // Step 3: Execute deletion
                    string deleteSql = "DELETE FROM Lessons WHERE Id = $id";

                    using var cmd = new SqliteCommand(deleteSql, connection);
                    cmd.Parameters.AddWithValue("$id", id);
                    cmd.ExecuteNonQuery();

                    changesPerformed = true;
                }

                if (changesPerformed)
                {
                    tableLessons.AcceptChanges();
                    MessageBox.Show("Changes have been saved successfully.", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDataIntoGridLessons();
            }
        }

        private bool CanDeleteLesson(int lessonId)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT COUNT(*) FROM StudentLessonLink 
                WHERE LessonId = $lessonId;
                ";
            command.Parameters.AddWithValue("$lessonId", lessonId);

            long count = (long)command.ExecuteScalar();

            return count == 0; // True if no lessons linked to the student
        }


        // =================
        // Invoices
        // =================

        private void LoadDataIntoGridInvoices()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string query = @"SELECT li.Id as Id, s.Name ||' (' || s.Parent || ') - ' || le.Name as Student, 
                             '£' || li.Price || ' ' || li.UsualDay || ' ' || li.UsualTime as Comment, 
                             strftime('%d-%m-%Y', DATE('now')) as LessonDate, li.UsualTime as LessonTime, strftime('%d-%m-%Y', DATE('now')) as InvoiceDate,
                             s.Name as StudentName, s.Parent as StudentParent, s.Email as StudentEmail, le.Name as LessonName, li.Price as Price
                             FROM StudentLessonLink li INNER JOIN Students s ON s.Id = li.StudentId INNER JOIN Lessons le ON le.Id = li.LessonId
                             WHERE li.Actual=true
                             ORDER BY Student";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            tableInvoices = new DataTable();
            tableInvoices.Load(reader);  // Load data from reader into DataTable
            tableInvoices.Columns.Add("Invoice", typeof(bool));

            dataGridViewInvoices.AutoGenerateColumns = true;
            dataGridViewInvoices.Columns.Clear();

            dataGridViewInvoices.DataSource = tableInvoices;

            dataGridViewInvoices.Columns["Student"].HeaderText = "Student - Lesson";
            dataGridViewInvoices.Columns["Comment"].HeaderText = "More Details";
            dataGridViewInvoices.Columns["LessonDate"].HeaderText = "Lesson Date";
            dataGridViewInvoices.Columns["LessonTime"].HeaderText = "Lesson Time";
            dataGridViewInvoices.Columns["InvoiceDate"].HeaderText = "Invoice Date";
/*            dataGridViewInvoices.Columns["Id"].Visible = false;
            dataGridViewInvoices.Columns["StudentName"].Visible = false;
            dataGridViewInvoices.Columns["StudentParent"].Visible = false;
            dataGridViewInvoices.Columns["StudentEmail"].Visible = false;
            dataGridViewInvoices.Columns["LessonName"].Visible = false;
            dataGridViewInvoices.Columns["Price"].Visible = false;*/

            dataGridViewInvoices.DataBindingComplete += dataGridViewInvoices_DataBindingComplete;
        }

        private void LoadDataIntoGridInvoicesLastMonth()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string query = @"SELECT s.Name ||' (' || s.Parent || ') - ' || le.Name as Student, 
                            '£' || li.Price || ' ' || li.UsualDay || ' ' || li.UsualTime as Comment, 
                            strftime('%d-%m-%Y', r.LessonDate) as LessonDate, r.LessonTime as LessonTime, strftime('%d-%m-%Y', r.InvoiceDate) as InvoiceDate, 
                            strftime('%d-%m %H:%M', r.InvoiceRecordedDate) as InvoiceRecordedDate
                            FROM StudentLessonLink li INNER JOIN Students s ON s.Id = li.StudentId INNER JOIN Lessons le ON le.Id = li.LessonId
                            INNER JOIN InvoiceRecords r ON li.Id = r.LinkId
                            WHERE strftime('%Y-%m', r.InvoiceRecordedDate) = strftime('%Y-%m', 'now')
                            ORDER BY r.InvoiceRecordedDate DESC";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            tableInvoicesLastMonth = new DataTable();
            tableInvoicesLastMonth.Load(reader);  // Load data from reader into DataTable

            dataGridViewInvoicesLastMonth.AutoGenerateColumns = true;
            dataGridViewInvoicesLastMonth.Columns.Clear();

            dataGridViewInvoicesLastMonth.DataSource = tableInvoicesLastMonth;

            dataGridViewInvoicesLastMonth.Columns["Student"].HeaderText = "Student - Lesson";
            dataGridViewInvoicesLastMonth.Columns["Comment"].HeaderText = "More Details";
            dataGridViewInvoicesLastMonth.Columns["LessonDate"].HeaderText = "Lesson Date";
            dataGridViewInvoicesLastMonth.Columns["LessonTime"].HeaderText = "Lesson Time";
            dataGridViewInvoicesLastMonth.Columns["InvoiceDate"].HeaderText = "Invoice Date";
            dataGridViewInvoicesLastMonth.Columns["InvoiceRecordedDate"].HeaderText = "Recorded At";

            dataGridViewInvoicesLastMonth.DataBindingComplete += dataGridViewInvoicesLastMonth_DataBindingComplete;
        }

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
                            strftime('%d-%m %H:%M', r.InvoiceRecordedDate) as InvoiceRecordedDate
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
                            strftime('%d-%m %H:%M', r.InvoiceRecordedDate) as InvoiceRecordedDate
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

        private void dataGridViewInvoices_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewInvoices.Columns["Student"].Width = 300;
            dataGridViewInvoices.Columns["Student"].ReadOnly = true;
            dataGridViewInvoices.Columns["Student"].DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridViewInvoices.Columns["Comment"].Width = 170;
            dataGridViewInvoices.Columns["Comment"].ReadOnly = true;
            dataGridViewInvoices.Columns["Comment"].DefaultCellStyle.BackColor = Color.AliceBlue;

            dataGridViewInvoices.Columns["Id"].Visible = false;
            dataGridViewInvoices.Columns["StudentName"].Visible = false;
            dataGridViewInvoices.Columns["StudentParent"].Visible = false;
            dataGridViewInvoices.Columns["StudentEmail"].Visible = false;
            dataGridViewInvoices.Columns["LessonName"].Visible = false;
            dataGridViewInvoices.Columns["Price"].Visible = false;

            dataGridViewInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridViewInvoicesLastMonth_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewInvoicesLastMonth.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridViewInvoicesLastMonth.Columns["Student"].Width = 300;
            dataGridViewInvoicesLastMonth.Columns["Comment"].Width = 170;
            dataGridViewInvoicesLastMonth.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridViewAllInvoices_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewAllInvoices.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridViewAllInvoices.Columns["Student"].Width = 300;
            dataGridViewAllInvoices.Columns["Comment"].Width = 170;
            dataGridViewAllInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonCancelChangesInv_Click(object sender, EventArgs e)
        {
            LoadDataIntoGridInvoices();
        }

        private void buttonSendInvoices_Click(object sender, EventArgs e)
        {
            SaveChangesAndSendInvoices();
        }

        private void SaveChangesAndSendInvoices()
        {
            // Step 1: Prepare a list of rows to process
            var rowsForSendingInvoices = tableInvoices.AsEnumerable()
                .Where(row => row["Invoice"] is bool isSelected && isSelected)
                .ToList();

            string selectedRowsData = string.Join(Environment.NewLine, rowsForSendingInvoices
                .Select(row => "- " + DataRowToString(row, new[] { "Student", "Comment", "LessonDate", "LessonTime", "InvoiceDate" })));
            
            if (selectedRowsData.Length == 0)
            {
                MessageBox.Show("No lines are indicated for sending invoices", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
            $"Do you really want to send these invoices? {Environment.NewLine}{selectedRowsData}", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var changesPerformed = false;
                foreach (var row in rowsForSendingInvoices)
                {
                    //check for missing values, wrong date formats etc!!!
                    string errorMessage;

                    if (!ValidationPassedDataRow(row, new List<string> { "Student", "Comment", "LessonDate", "LessonTime", "InvoiceDate" },
                    new List<string> { "LessonDate", "InvoiceDate" }, new List<string> { }, out errorMessage))
                    {
                        MessageBox.Show($"Errors in line {DataRowToString(row, new[] { "Student", "Comment", "LessonDate", "LessonTime", "InvoiceDate" })}{Environment.NewLine}{errorMessage}",
                                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        using var connection = new SqliteConnection(connectionString);
                        connection.Open();

                        // Convert to SQLite-compatible format
                        string sqliteLessonDate = ToDateFormat(row["LessonDate"], "dd-mm-yyyy", "yyyy-MM-dd");
                        string sqliteInvoiceDate = ToDateFormat(row["InvoiceDate"], "dd-mm-yyyy", "yyyy-MM-dd");

                        string insertSql = @"INSERT INTO InvoiceRecords (LinkId, LessonDate, LessonTime, InvoiceDate, InvoiceRecordedDate)
                        VALUES ($linkId, $lessonDate, $lessonTime, $invoiceDate, datetime('now', 'localtime'))";

                        using var cmd = new SqliteCommand(insertSql, connection);
                        cmd.Parameters.AddWithValue("$linkId", row["Id"]);
                        cmd.Parameters.AddWithValue("$lessonDate", sqliteLessonDate);
                        cmd.Parameters.AddWithValue("$lessonTime", row["LessonTime"]);
                        cmd.Parameters.AddWithValue("$invoiceDate", sqliteInvoiceDate);

                        cmd.ExecuteNonQuery();
                        changesPerformed = true;

                        // when line is added invoice can be sent
                        var replacements = new Dictionary<string, string>
                        {
                            { "{studentName}", row["StudentName"].ToString() },
                            { "{parentName}", row["StudentParent"].ToString() },
                            { "{lessonName}", row["LessonName"].ToString() },
                            { "{lessonTime}", row["LessonTime"].ToString() },
                            { "{price}", $"£{row["Price"].ToString()}" },
                            { "{lessonDate}", ToDateFormat(row["LessonDate"],"dd-mm-yyyy","dd/mm/yyyy") },  
                            { "{invoiceDate}", ToDateFormat(row["InvoiceDate"],"dd-mm-yyyy","dd/mm/yyyy") },
                        };
                        
                        string templateFile = templatePath + "invoiceTemplate.docx";
                        string outputFileName = $"invoice_{row["StudentName"].ToString()}_{ToDateFormat(row["LessonDate"],"yyyy-mm-dd","ddmmyyyy")}";

                        string outputPath = connectionString == "Data Source=tutorhelper.db"?@$"{invoicesPath}Invoices\{outputFileName}.docx": @$"{invoicesPath}InvoicesTest\{outputFileName}.docx";
                        string outputPathPdf = connectionString == "Data Source=tutorhelper.db" ? @$"{invoicesPath}Invoices\{outputFileName}.pdf" : @$"{invoicesPath}InvoicesTest\{outputFileName}.pdf";

                        WordTemplateHelper.ReplacePlaceholders(templateFile, outputPath, replacements);
                        WordToPdfConverter.Convert(outputPath, outputPathPdf);

                        EmailHelper.SendInvoice(row["StudentEmail"].ToString(), row["StudentParent"].ToString(), ToDateFormat(row["InvoiceDate"], "yyyy-mm-dd", "ddmmyyyy"), outputPathPdf);

                        Cursor.Current = Cursors.Default;
                    }
                }
                if (changesPerformed)
                {
                    MessageBox.Show("Changes have been saved successfully.", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadDataIntoGridInvoices();
                LoadDataIntoGridInvoicesLastMonth();
            }
        }

        private string ToDateFormat(object date, string formatFrom, string formatTo)
        {
            string dateStr = date.ToString();

            if (DateTime.TryParseExact(dateStr, formatFrom, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue))
            {
                string formattedDate = dateValue.ToString(formatTo);
                return formattedDate;
            }
            return null;
        }


        // =================
        // Generic methods
        // =================

        private void Generic_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (sender is DataGridView grid && e.Row.DataBoundItem is DataRowView rowView)
            {
                rowView["IsMarkedDeleted"] = true;
                e.Cancel = true;
                grid.InvalidateRow(e.Row.Index); // Repaint the row to show visual change
            }
        }

        private void Generic_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var row = grid.Rows[e.RowIndex];

            if (row.DataBoundItem is DataRowView rowView)
            {
                var dataRow = rowView.Row;

                // Check if the row is marked as deleted
                if (dataRow.Table.Columns.Contains("IsMarkedDeleted") &&
                    dataRow["IsMarkedDeleted"] is bool isDeleted && isDeleted)
                {
                    // Apply strikeout font style
                    row.DefaultCellStyle.Font = new Font(grid.DefaultCellStyle.Font, FontStyle.Strikeout);
                }
                else
                {
                    // Optional: Reset to normal if not deleted
                    row.DefaultCellStyle.Font = new Font(grid.DefaultCellStyle.Font, FontStyle.Regular);
                }
            }
        }

        private void Generic_SetupTabStylesAndLoadGrid(TabPage tab, DataGridView grid, Action loadGrid )
        {
            tab.BackColor = Color.WhiteSmoke;
            grid.BackgroundColor = Color.WhiteSmoke;
            loadGrid();
            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                BackColor = Color.Lavender,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            grid.EnableHeadersVisualStyles = false; // Required to apply BackColor
        }

        private void AllowDBNullToDataTable(DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                column.AllowDBNull = true;
            }
        }

        string DataRowToString (DataRow row, string[] columnNames)
        {
            return string.Join(", ", columnNames.Select(col => row[col]?.ToString() ?? string.Empty));
        }

        bool ValidationPassedDataRow(DataRow row, List<string> columnNames, List<string> columnWithDates, List<string> columnWithNumbers, out string errorMessage)
        {
            errorMessage = string.Empty;
            string emptyValues = "";
            string wrongFormatValues = "";
            bool result = true;
            foreach (var colName in columnNames)
            {
                var data = row[colName];
                if (data?.ToString() == string.Empty)
                {
                    emptyValues += (colName + " ");
                    result = false;
                }
                else
                {
                    if (columnWithDates.Contains(colName))
                    {
                        if (!DateTime.TryParse(data.ToString(), out DateTime dateValue))
                        {
                            wrongFormatValues += (colName + " ");
                            result = false;
                        }
                    }
                    else if (columnWithNumbers.Contains(colName))
                    {
                        if (!decimal.TryParse(data.ToString(), out decimal number))
                        {
                            wrongFormatValues += (colName + " ");
                            result = false;
                        }

                    }
                }
            }
            errorMessage += (emptyValues == string.Empty ? "" : $"Missed values in columns: {emptyValues}{Environment.NewLine}");
            errorMessage += (wrongFormatValues == string.Empty ? "" : $"Wrong format values in columns: {wrongFormatValues}{Environment.NewLine}");
            return result;
        }

        // =================
        // For filling combo boxes
        // =================

        public DataTable GetCurrentStudents()
        {
            // Create DataTable with schema
            DataTable studentsTable = new DataTable();
            studentsTable.Columns.Add("Id", typeof(long));
            studentsTable.Columns.Add("Name", typeof(string));

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT Id, Name || ' (' || Parent || ')' 
                FROM Students 
                WHERE Current=true
                ORDER BY Name";


                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    studentsTable.Rows.Add(id, name);
                }
            }
            return studentsTable;
        }

        public DataTable GetAllStudents()
        {
            // Create DataTable with schema
            DataTable studentsTable = new DataTable();
            studentsTable.Columns.Add("Id", typeof(long));
            studentsTable.Columns.Add("Name", typeof(string));

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT Id, Name || ' (' || Parent || ')' 
                FROM Students 
                ORDER BY Name";


                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    studentsTable.Rows.Add(id, name);
                }
            }
            return studentsTable;
        }

        public DataTable GetLessons()
        {
            // Create DataTable with schema
            DataTable lessonsTable = new DataTable();
            lessonsTable.Columns.Add("Id", typeof(long));
            lessonsTable.Columns.Add("Name", typeof(string));

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT Id, Name FROM Lessons ORDER BY Name";

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    lessonsTable.Rows.Add(id, name);
                }
            }
            return lessonsTable;
        }

        public DataTable GetAllYears()
        {
            // Create DataTable with schema
            DataTable yearsTable = new DataTable();
            yearsTable.Columns.Add("Name", typeof(string));

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT distinct strftime('%Y', LessonDate) as Year
                FROM InvoiceRecords 
                ORDER BY Year";

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string year = reader.GetString(0);
                    yearsTable.Rows.Add(year);
                }
            }
            return yearsTable;
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

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridAllInvoices();
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridAllInvoices();
        }
    }
}