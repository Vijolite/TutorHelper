using Microsoft.Data.Sqlite;
using System.Data;

namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
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
    }
}
