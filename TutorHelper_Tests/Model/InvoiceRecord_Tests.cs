using TutorHelper.Model;

namespace TutorHelper_Tests.Model
{
    public class HtmlTemplateHelper_Tests
    {

        [Theory]
        [InlineData("Anna", 2025, 2, 5, "invoice_Anna_05022025")]
        [InlineData("Maris", 2025, 12, 15, "invoice_Maris_15122025")]
        public void CustomizeInvoiceFileName_ReturnsExpectedResult(string studentName, int year, int month, int day, string expectedResult)
        {
            // Arrange
            var student = new Student(studentName, "TestParent", "test@email", true);
            var lesson = new Lesson("TestLesson");
            var invoiceRecord = new InvoiceRecord
            {
                StudentLessonLink = new StudentLessonLink (student, lesson, 10m, null!, new TimeOnly(0, 0)),
                LessonDate = new DateOnly(year, month, day),
                LessonTime = new TimeOnly(10, 30),
                InvoiceDate = new DateOnly(2025, 10, 10),
            };

            // Act
            string result = invoiceRecord.CustomizeInvoiceFileName();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
