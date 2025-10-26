using Spire.Doc;

namespace TutorHelper.Helpers
{
    public class HtmlToWordConverter
    {
        public static void Convert(string htmlFilePath, string docFilePath)
        {
            Document document = new Document(htmlFilePath);

            document.SaveToFile(docFilePath, FileFormat.Docx);
        }
    }
}
