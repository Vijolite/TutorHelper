using System.Data;
using System.Globalization;

namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
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

        string SearchForRightFolderForReportWithDate(string mainFolderPath, string prefix, string year, string month)
        {
            if (!Directory.Exists(mainFolderPath))
            {
                Directory.CreateDirectory(mainFolderPath);
            }

            string folderPath = @$"{mainFolderPath}\{prefix}_{year}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            folderPath = @$"{folderPath}\{prefix}_{year}{month}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        string SearchForRightFolderForReportWithDate(string mainFolderPath, string prefix, string year)
        {
            if (!Directory.Exists(mainFolderPath))
            {
                Directory.CreateDirectory(mainFolderPath);
            }

            string folderPath = @$"{mainFolderPath}\{prefix}_{year}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

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

        private void Generic_SetupTabStylesAndLoadGrid(TabPage tab, DataGridView grid, Action loadGrid)
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

        string DataRowToString(DataRow row, string[] columnNames)
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
    }
}
