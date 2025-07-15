
using Microsoft.Data.Sqlite;
using System.Configuration;

namespace TutorHelper.DataBaseConnection
{
    public class Sqlite
    {
        static string connectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];

        public static void InsertRecord(string table, Dictionary<string,string> valuesToInsert)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var insertCmd = connection.CreateCommand();

            string listOfColumns = string.Join(",", valuesToInsert.Keys);
            string listOfVariables = string.Join(",", valuesToInsert.Keys.Select(k => $"${k}"));

            insertCmd.CommandText = $@"
    INSERT INTO {table} ({listOfColumns})
    VALUES ({listOfVariables});
";
            foreach (var pair in valuesToInsert)
            {
                insertCmd.Parameters.AddWithValue("$"+pair.Key, pair.Value);
            }
            insertCmd.ExecuteNonQuery();

            // Get last inserted ID with a new command
            var idCmd = connection.CreateCommand();
            idCmd.CommandText = "SELECT last_insert_rowid();";
            long insertedId = (long)(idCmd.ExecuteScalar() ?? 0);

            Console.WriteLine($"New student ID: {insertedId}");
        }

          
    }
}

