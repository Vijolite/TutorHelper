using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.Sqlite;
using System.Data;
using TutorHelper.Helpers;
using Color = System.Drawing.Color;


namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
        private void LoadDataIntoGridInvoices()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string query = @"SELECT li.Id as Id, s.Name ||' (' || s.Parent || ') - ' || le.Name as Student, 
                             '£' || li.Price || ' ' || li.UsualDay || ' ' || li.UsualTime as Comment, 
                             strftime('%d-%m-%Y', DATE('now')) as LessonDate, li.UsualTime as LessonTime, strftime('%d-%m-%Y', DATE('now')) as InvoiceDate,
                             s.Name as StudentName, s.Parent as StudentParent, s.Email as StudentEmail, le.Name as LessonName, li.Price as Price,
                             li.ZoomInvite as ZoomInviteText
                             FROM StudentLessonLink li INNER JOIN Students s ON s.Id = li.StudentId INNER JOIN Lessons le ON le.Id = li.LessonId
                             WHERE li.Actual=true
                             ORDER BY Student";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            tableInvoices = new DataTable();
            tableInvoices.Load(reader);  // Load data from reader into DataTable
           
            tableInvoices.Columns.Add("Invoice", typeof(bool));
            tableInvoices.Columns.Add("Zoom", typeof(bool));
            tableInvoices.Columns.Add("EmailSubject", typeof(string));

            dataGridViewInvoices.AutoGenerateColumns = true;
            dataGridViewInvoices.Columns.Clear();

            dataGridViewInvoices.DataSource = tableInvoices;

            dataGridViewInvoices.Columns["Student"].HeaderText = "Student - Lesson";
            dataGridViewInvoices.Columns["Comment"].HeaderText = "More Details";
            dataGridViewInvoices.Columns["LessonDate"].HeaderText = "Lesson Date";
            dataGridViewInvoices.Columns["LessonTime"].HeaderText = "Lesson Time";
            dataGridViewInvoices.Columns["InvoiceDate"].HeaderText = "Invoice Date";
            dataGridViewInvoices.Columns["EmailSubject"].HeaderText = "Email Subject";

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

        private void dataGridViewInvoices_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewInvoices.Columns["Student"].ReadOnly = true;
            dataGridViewInvoices.Columns["Student"].DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridViewInvoices.Columns["Comment"].ReadOnly = true;
            dataGridViewInvoices.Columns["Comment"].DefaultCellStyle.BackColor = Color.AliceBlue;

            dataGridViewInvoices.Columns["Id"].Visible = false;
            dataGridViewInvoices.Columns["StudentName"].Visible = false;
            dataGridViewInvoices.Columns["StudentParent"].Visible = false;
            dataGridViewInvoices.Columns["StudentEmail"].Visible = false;
            dataGridViewInvoices.Columns["LessonName"].Visible = false;
            dataGridViewInvoices.Columns["Price"].Visible = false;
            dataGridViewInvoices.Columns["ZoomInviteText"].Visible = false;

            dataGridViewInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewInvoices.Columns["Student"].FillWeight = 20;
            dataGridViewInvoices.Columns["Comment"].FillWeight = 13;
            dataGridViewInvoices.Columns["LessonDate"].FillWeight = 11;
            dataGridViewInvoices.Columns["LessonTime"].FillWeight = 11;
            dataGridViewInvoices.Columns["InvoiceDate"].FillWeight = 11;
            dataGridViewInvoices.Columns["Invoice"].FillWeight = 7;
            dataGridViewInvoices.Columns["Zoom"].FillWeight = 7;
            dataGridViewInvoices.Columns["EmailSubject"].FillWeight = 20;
        }

        private void dataGridViewInvoicesLastMonth_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewInvoicesLastMonth.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridViewInvoicesLastMonth.Columns["Student"].Width = 300;
            dataGridViewInvoicesLastMonth.Columns["Comment"].Width = 170;
            dataGridViewInvoicesLastMonth.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //to react to bool selection immediately
        private void dataGridViewInvoices_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridViewInvoices.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = dataGridViewInvoices.Columns[e.ColumnIndex];
            if (col.Name == "Zoom")
            {
                var row = dataGridViewInvoices.Rows[e.RowIndex];
                string zoomText = row.Cells["ZoomInviteText"].Value?.ToString() ?? "";

                // Check BEFORE committing value
                if (string.IsNullOrEmpty(zoomText))
                {
                    MessageBox.Show(
                        $"Errors in line {row.Cells["Student"].Value} - {row.Cells["Comment"].Value}{Environment.NewLine}Zoom Invite text should be entered at StudentLessonLink tab!",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Cancel the click: do NOT toggle checkbox
                    dataGridViewInvoices.CancelEdit(); // resets checkbox immediately
                    return;
                }

                // If valid, commit edit manually
                dataGridViewInvoices.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridViewInvoices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridViewInvoices.Columns[e.ColumnIndex].Name == "Invoice" || dataGridViewInvoices.Columns[e.ColumnIndex].Name == "Zoom" ||
                dataGridViewInvoices.Columns[e.ColumnIndex].Name == "LessonDate" || dataGridViewInvoices.Columns[e.ColumnIndex].Name == "InvoiceDate" ||
                dataGridViewInvoices.Columns[e.ColumnIndex].Name == "LessonTime")
            {
                var row = dataGridViewInvoices.Rows[e.RowIndex];

                bool invoice = row.Cells["Invoice"].Value as bool? == true;
                bool zoom = row.Cells["Zoom"].Value as bool? == true;

                if (invoice && zoom)
                {
                    string weekDay = ToDateFormat(row.Cells["LessonDate"].Value, "dd-MM-yyyy", "dddd");
                    var lessonDate = ToDateFormat(row.Cells["LessonDate"].Value, "dd-MM-yyyy", "dd.MM");
                    row.Cells["EmailSubject"].Value = $"Zoom link and invoice for {weekDay} {lessonDate} {row.Cells["LessonTime"].Value}";
                }
                else if (invoice)
                {
                    row.Cells["EmailSubject"].Value = $"invoice {ToDateFormat(row.Cells["InvoiceDate"].Value, "dd-MM-yyyy", "ddMMyyyy")}";
                }
                else if (zoom)
                {
                    row.Cells["EmailSubject"].Value = $"Lesson tomorrow {row.Cells["LessonTime"].Value}";
                }
                else
                {
                    row.Cells["EmailSubject"].Value = null;
                }
            }
        }

        private void buttonCancelChangesInv_Click(object sender, EventArgs e)
        {
            LoadDataIntoGridInvoices();
        }

        private void buttonSendInvoices_Click(object sender, EventArgs e)
        {
            SaveChangesAndSendInvoicesOrZoomInvitations();
        }

        private void SaveChangesAndSendInvoicesOrZoomInvitations()
        {
            // Step 1: Prepare a list of rows to process
            var rowsForSendingInvoices = tableInvoices.AsEnumerable()
                .Where(row => row["Invoice"] is bool isSelectedInvoice && isSelectedInvoice 
                || row["Zoom"] is bool isSelectedZoom && isSelectedZoom)
                .ToList();

            string selectedRowsData = string.Join(Environment.NewLine, rowsForSendingInvoices
                .Select(row => "- " + DataRowToString(row, new[] { "Student", "Comment", "LessonDate", "LessonTime", "InvoiceDate", "EmailSubject" }) 
                + Environment.NewLine));

            if (selectedRowsData.Length == 0)
            {
                MessageBox.Show("No lines are indicated for sending invoices/Zoom invitations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
            $"Do you really want to send these invoices/Zoom invitations? {Environment.NewLine}{Environment.NewLine}{selectedRowsData}", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var changesPerformed = false;
                Cursor.Current = Cursors.WaitCursor;
                foreach (var row in rowsForSendingInvoices)
                {
                    //check for missing values, wrong date formats etc!!!
                    string errorMessage;

                    if (!ValidationPassedDataRow(row, new List<string> { "Student", "Comment", "LessonDate", "LessonTime", "InvoiceDate", "EmailSubject" },
                    new List<string> { "LessonDate", "InvoiceDate" }, new List<string> { }, out errorMessage))
                    {
                        MessageBox.Show($"Errors in line {DataRowToString(row, new[] { "Student", "Comment", "LessonDate", "LessonTime", "InvoiceDate", "EmailSubject" })}{Environment.NewLine}{errorMessage}",
                                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    else
                    {
                        bool isInvoice = row.Field<bool?>("Invoice") == true;
                        bool isZoom = row.Field<bool?>("Zoom") == true;

                        if (isInvoice)
                        {
                            using var connection = new SqliteConnection(connectionString);
                            connection.Open();

                            // Convert to SQLite-compatible format
                            string sqliteLessonDate = ToDateFormat(row["LessonDate"], "dd-MM-yyyy", "yyyy-MM-dd");
                            string sqliteInvoiceDate = ToDateFormat(row["InvoiceDate"], "dd-MM-yyyy", "yyyy-MM-dd");

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
                            { "{lessonDate}", ToDateFormat(row["LessonDate"],"dd-MM-yyyy","dd/MM/yyyy") },
                            { "{invoiceDate}", ToDateFormat(row["InvoiceDate"],"dd-MM-yyyy","dd/MM/yyyy") },
                        };

                            string templateFile = templatePath + "invoiceTemplate.docx";

                            string outputFileName = $"invoice_{row["StudentName"].ToString()}_{ToDateFormat(row["LessonDate"], "dd-MM-yyyy", "ddMMyyyy")}";
                            if (IfStudentWithSeveralLessons((long)row["Id"])) outputFileName += $"_{row["LessonName"].ToString()}";

                            string mainOutputFolder = @$"{invoicesPath}{invoicesFolderName}";
                            string outputFolderPath = SearchForRightFolderForReportWithDate(mainOutputFolder, invoicesFolderName, ToDateFormat(row["LessonDate"], "dd-MM-yyyy", "yyyy"), ToDateFormat(row["LessonDate"], "dd-MM-yyyy", "MM"));

                            string outputPath = @$"{outputFolderPath}\{outputFileName}.docx";
                            string outputPathPdf = @$"{outputFolderPath}\{outputFileName}.pdf";

                            WordTemplateHelper.ReplacePlaceholders(templateFile, outputPath, replacements);
                            WordToPdfConverter.Convert(outputPath, outputPathPdf);

                            string body = isInvoice && isZoom ? row["ZoomInviteText"].ToString() 
                                : $"Invoice from date: {ToDateFormat(row["InvoiceDate"], "dd-MM-yyyy", "ddMMyyyy")} is attached";

                            EmailHelper.SendInvoice(row["StudentEmail"].ToString(), row["StudentParent"].ToString(), row["EmailSubject"].ToString(), body, outputPathPdf);
                        }
                        else 
                        {
                            EmailHelper.SendZoomInvite(row["StudentEmail"].ToString(), row["StudentParent"].ToString(), row["EmailSubject"].ToString(), row["ZoomInviteText"].ToString());
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
                if (changesPerformed)
                {
                    MessageBox.Show("Changes have been saved successfully.", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadDataIntoGridInvoices();
                LoadDataIntoGridInvoicesLastMonth();
            }
        }

        private bool IfStudentWithSeveralLessons(long linkId)
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*)
                                    FROM StudentLessonLink
                                    WHERE Actual=true
                                    AND StudentId IN (
                                    SELECT li.StudentId FROM StudentLessonLink li INNER JOIN InvoiceRecords r ON li.Id = r.LinkId
                                    WHERE r.LinkId = @linkId
                                    )";
                cmd.Parameters.AddWithValue("@linkId", linkId);

                long count = (long)cmd.ExecuteScalar();
                return count > 1;
            }

        }

    }
}
