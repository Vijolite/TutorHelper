using System.Data;

namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
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
    }
}
