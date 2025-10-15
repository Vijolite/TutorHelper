using Microsoft.Data.Sqlite;
using System.Data;

namespace TutorHelper.Forms
{
    public partial class MainForm : Form
    {
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
    }
}
