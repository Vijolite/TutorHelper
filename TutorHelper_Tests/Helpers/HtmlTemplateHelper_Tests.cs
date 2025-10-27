using TutorHelper.Helpers;

namespace TutorHelper_Tests.Helpers
{
    public class HtmlTemplateHelper_Tests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[]
            {
            new Dictionary<string, string> { { "{studentName}", "Anna" }, { "{lessonDate1}", "20/10/2025" },{ "{price1}", "£25" },
            { "{lessonDate2}", "25/10/2025" },{ "{price2}", "£28" }},
            "<tr><td>20/10/2025</td><td>£25</td><td></td></tr><tr><td>25/10/2025</td><td>£28</td><td></td></tr>"
            };

            yield return new object[]
            {
            new Dictionary<string, string> { { "{studentName}", "Anna" }, { "{parentName}", "Maria" } },
            ""
            };
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void PrepareLessonPriceTable_ReturnsExpectedResult(Dictionary<string, string> input, string expectedResult)
        {
            // Arrange

            // Act
            string result = HtmlTemplateHelper.PrepareLessonPriceTable(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

    }
}
