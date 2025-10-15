using Microsoft.Data.Sqlite;
using System.Data;

namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
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


    }
}
