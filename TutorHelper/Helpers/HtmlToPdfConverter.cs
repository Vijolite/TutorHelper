using Spire.Doc;

namespace TutorHelper.Helpers
{
    public class HtmlToPdfConverter
    {
        public static void Convert(string htmlFilePath, string pdfFilePath)
        {
            Document document = new Document(htmlFilePath);

            document.SaveToFile(pdfFilePath, FileFormat.PDF);
        }
    }
}
