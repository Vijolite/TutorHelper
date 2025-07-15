using Spire.Doc;

namespace TutorHelper.Helpers
{
    public class WordToPdfConverter
    {
        public static void Convert(string docFilePath, string pdfFilePath)
        {
            Document doc = new Document();
            doc.LoadFromFile(docFilePath);
            doc.SaveToFile(pdfFilePath, FileFormat.PDF);
        }
    }
}
