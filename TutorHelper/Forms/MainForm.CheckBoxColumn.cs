namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
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
    }
}
