using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Threading.Tasks;


namespace ClientLogin.SendEmailService
{
    public class SendEmailService
    {

        public static void Main()
        {

        }
        public static void SendEmail(string ToEmail, string ToName, string Subject)
        {
            Execute(ToEmail, ToName, Subject).Wait();
        }


        static async Task Execute(string ToEmail,string ToName,string Subject)
        {
            var apiKey = ConfigurationManager.AppSettings["emailServiceAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", ToName);
            var to = new EmailAddress(ToEmail, ToName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, Subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
        
    }
}