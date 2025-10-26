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

    }
}
