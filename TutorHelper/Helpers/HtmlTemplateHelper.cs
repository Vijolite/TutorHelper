namespace TutorHelper.Helpers
{
    public class HtmlTemplateHelper
    {
        public static void ReplacePlaceholders(string templatePath, string outputPath, Dictionary<string, string> replacements)
        {
            string fileContent = File.ReadAllText(templatePath);

            foreach (var entry in replacements)
            {
                fileContent = fileContent.Replace(entry.Key, entry.Value);
            }

            File.WriteAllText(outputPath, fileContent);
        }

        public static string PrepareLessonPriceTable (Dictionary<string, string> replacements)
        {
            var lessonDates = replacements.Where(x => x.Key.StartsWith("{lessonDate")).ToDictionary(x => x.Key, x => x.Value);
            var prices = replacements.Where(x => x.Key.StartsWith("{price")).ToDictionary(x => x.Key, x => x.Value);

            var number = lessonDates.Count();

            string htmlInsert = "";
            for (int i = 1; i < number+1; i++)
            {
                string ld = lessonDates[@$"{{lessonDate{i.ToString()}}}"];
                string p = prices[@$"{{price{i.ToString()}}}"];
                string htmlTableRow = @$"<tr><td>{ld}</td><td>{p}</td><td></td></tr>";
                htmlInsert += htmlTableRow;
            }
            return htmlInsert;

        }

        public static string PrepareYearReportMonthTable(Dictionary<string, string> replacements)
        {
            var lessonDates = replacements.Where(x => x.Key.StartsWith("{lessonDate")).ToDictionary(x => x.Key, x => x.Value);
            var prices = replacements.Where(x => x.Key.StartsWith("{price")).ToDictionary(x => x.Key, x => x.Value);
            var students = replacements.Where(x => x.Key.StartsWith("{studentName")).ToDictionary(x => x.Key, x => x.Value);

            var number = lessonDates.Count();

            string htmlInsert = "";
            for (int i = 1; i < number + 1; i++)
            {
                string ld = lessonDates[@$"{{lessonDate{i.ToString()}}}"];
                string p = prices[@$"{{price{i.ToString()}}}"];
                string st = students[@$"{{studentName{i.ToString()}}}"];
                string htmlTableRow = @$"<tr><td>{ld}</td><td>{st}</td><td>{p}</td></tr>";
                htmlInsert += htmlTableRow;
            }
            return htmlInsert;

        }

        public static string PrepareYearReportGroupedMonthTables(Dictionary<string, string> replacements)
        {
            var months = replacements.Where(x => x.Key.StartsWith("{month")).ToDictionary(x => x.Key, x => x.Value);
            var htmls = replacements.Where(x => x.Key.StartsWith("{htmlForInnerTable")).ToDictionary(x => x.Key, x => x.Value);
            var totals = replacements.Where(x => x.Key.StartsWith("{total")).ToDictionary(x => x.Key, x => x.Value);

            var number = months.Count();

            string htmlInsert = "";
            for (int i = 1; i < number + 1; i++)
            {
                string mon = months[@$"{{month{i.ToString()}}}"];
                string html = htmls[@$"{{htmlForInnerTable{i.ToString()}}}"];
                string total = totals[@$"{{total{i.ToString()}}}"];
                string htmlTableRow = @$"<h2>{mon}</h2><table>{html}<tr><th>Total</th><th></th><th>{total}</th></tr></table><br>";
                htmlInsert += htmlTableRow;
            }
            return htmlInsert;

        }
        

    }
}
