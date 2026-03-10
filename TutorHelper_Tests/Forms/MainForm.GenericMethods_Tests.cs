using System.Data;
using System.Windows.Forms;

namespace TutorHelper_Tests.Forms
{
    public class GenericMethods_Tests
    {

        public static IEnumerable<object[]> GetTestData()
        {
            DateTime dt = DateTime.Now;

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(string));
            table.Columns.Add("LessonDate", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("EmailAdditional", typeof(string));

            DataRow row1 = table.NewRow();
            row1["Name"] = "John";
            row1["Age"] = "15";
            row1["LessonDate"] = dt.ToString();
            row1["Email"] = "john@gmail.com";
            row1["EmailAdditional"] = "john1@gmail.com";

            table.Rows.Add(row1);

            yield return new object[]
            {
                row1,
                new List<string> { "Name", "LessonDate", "Email" },
                new List<string> { "EmailAdditional", "Age" },
                new List<string> { "LessonDate" },
                new List<string> { "Age" },
                new List<string> { "EmailAdditional", "EmailAdditional" },
                true
            };

            DataRow row2 = table.NewRow();
            row2["Name"] = "John";
            row2["Age"] = "aaa";            
            row2["LessonDate"] = dt.ToString();
            row2["Email"] = "";
            row2["EmailAdditional"] = "";

            table.Rows.Add(row2);

            yield return new object[]
            {
                row2,
                new List<string> { "Name", "LessonDate", "Email" },
                new List<string> { "EmailAdditional", "Age" },
                new List<string> { "LessonDate" },
                new List<string> { "Age" },
                new List<string> { "EmailAdditional", "EmailAdditional" },
                false
            };

        }


        [Theory]
        [MemberData(nameof(GetTestData))]
        public void ValidationPassedDataRow_ReturnsExpectedResult(DataRow row, List<string> columnNamesEssentual, List<string> columnNamesAdditional,
            List<string> columnWithDates, List<string> columnWithNumbers, List<string> columnWithEmails, bool expectedResult)
        {
            // Arrange
            string errorMessage;

            // Act
            bool result = TutorHelper.Forms.MainForm.ValidationPassedDataRow(row, columnNamesEssentual, columnNamesAdditional,
            columnWithDates, columnWithNumbers, columnWithEmails, out errorMessage);

            // Assert
            Assert.Equal(expectedResult, result);

        }

        

    }
}
