using DocumentFormat.OpenXml.Packaging;

public class WordTemplateHelper
{
    public static void ReplacePlaceholders(string templatePath, string outputPath, Dictionary<string, string> replacements)
    {
        File.Copy(templatePath, outputPath, true);

        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputPath, true))
        {
            ReplaceInPart(wordDoc.MainDocumentPart, replacements);

            foreach (var headerPart in wordDoc.MainDocumentPart.HeaderParts)
                ReplaceInPart(headerPart, replacements);

            foreach (var footerPart in wordDoc.MainDocumentPart.FooterParts)
                ReplaceInPart(footerPart, replacements);
        }
    }

    private static void ReplaceInPart(OpenXmlPart part, Dictionary<string, string> replacements)
    {
        var texts = part.RootElement.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>();

        foreach (var text in texts)
        {
            foreach (var pair in replacements)
            {
                if (text.Text.Contains(pair.Key))
                {
                    text.Text = text.Text.Replace(pair.Key, pair.Value);
                }
            }
        }
    }
}



