using System.Data;
using Color = System.Drawing.Color;
using System.Configuration;

namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
        static string connectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
        string templatePath = ConfigurationManager.AppSettings["InvoiceTemplatePath"];
        string invoicesPath = ConfigurationManager.AppSettings["PreparedInvoicesPath"];
        string invoicesFolderName = connectionString == "Data Source=tutorhelper.db" ? @$"Invoices" : @$"InvoicesTest";
        string summariesFolderName = connectionString == "Data Source=tutorhelper.db" ? @$"Summaries" : @$"SummariesTest";

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


        // =================
        // StudentLessonLink
        // =================
        

        // =================
        // Combo Box Column
        // =================


        // =================
        // Check Box Column
        // =================


        // =================
        // Lessons
        // =================


        // =================
        // Invoices
        // =================


        // =================
        // Generic methods
        // =================
        

        // =================
        // For filling combo boxes
        // =================


    }
}