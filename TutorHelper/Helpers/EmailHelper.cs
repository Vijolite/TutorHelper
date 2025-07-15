using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace TutorHelper.Helpers
{
    public class EmailHelper
    {
        static string email = ConfigurationManager.AppSettings["EmailFrom"];
        static string sender = ConfigurationManager.AppSettings["NameFrom"];

        static MailAddress fromAddress = new MailAddress(email, sender);
        static string fromPassword = Environment.GetEnvironmentVariable("TUTOR_HELPER_EMAIL_PASSWORD");
        public static SmtpClient InitializeSmtpClient()
        {
            return new SmtpClient
            {
                Host = "smtp.gmail.com",      // e.g., smtp.gmail.com
                Port = 587,                     // or 465 depending on provider
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
        }
        public static void SendEmail (MailAddress toAddress, string subject, string body, string fileToAttach=null!)
        {
            var smtp = InitializeSmtpClient();

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            // 📎 Add attachment
            if (fileToAttach != null)
            {
                message.Attachments.Add(new Attachment(fileToAttach));
            }

            // ✉️ Send email
            smtp.Send(message);

            Console.WriteLine("Email sent.");

        }
        public static void SendInvoice(string recipientEmail, string recipientName, DateOnly invoiceDate, string fileToAttach)
        {
            var toAddress = new MailAddress(recipientEmail, recipientName);
            string subject = $"invoice {invoiceDate.ToString()}";
            string body = $"Invoice from date: {invoiceDate.ToString()} is attached";

            SendEmail(toAddress, subject, body, fileToAttach);
        }

        public static void SendInvoice(string recipientEmail, string recipientName, string invoiceDate, string fileToAttach)
        {
            var toAddress = new MailAddress(recipientEmail, recipientName);
            string subject = $"invoice {invoiceDate}";
            string body = $"Invoice from date: {invoiceDate} is attached";

            SendEmail(toAddress, subject, body, fileToAttach);
        }

    }
}
