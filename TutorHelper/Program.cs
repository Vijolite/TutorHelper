using System.Configuration;
using TutorHelper.Forms;
using TutorHelper.Helpers;

namespace TutorHelper
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var replacements = new Dictionary<string, string>
                        {
                            { "{studentName}", "Anna" },
                            { "{parentName}", "Annas mom" },
                            { "{lessonName}", "new lesson" }

                        };

            string templatePath = ConfigurationManager.AppSettings["InvoiceTemplatePath"];
            string templateFile = templatePath + "summaryTemplate.html";
            string withReplacements = templatePath + "invoice.html";

            HtmlTemplateHelper.ReplacePlaceholders(templateFile, withReplacements, replacements);

            string outputFileName = $"html_doc";
            string outputPath = @$"{templatePath}\{outputFileName}.docx";
            string outputPathPdf = @$"{templatePath}\{outputFileName}.pdf";


            HtmlToWordConverter.Convert(withReplacements, outputPath);
            //WordToPdfConverter.Convert(outputPath, outputPathPdf);

            //HtmlToPdfConverter.Convert(withReplacements, outputPathPdf);

            //CreateWordDocFromTemplate(templateType, outputFolderPath, outputFileName, replacements);






            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}


