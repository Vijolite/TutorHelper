using Microsoft.Data.Sqlite;
using System.Data;

namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
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
    }
}
