using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace ClientLogin.SendEmailService
{
    public class SendEmailService
    {
        
        public static void Main()
        {

        }
        public static void SendEmail(string ToEmail, string ToName, string Subject,string Username)
        {
            Execute(ToEmail, ToName, Subject, ToEmail);
        }

        public static void Execute(string ToEmail, string ToName, string Subject, string Username)
        {
            string AccountCreated = HttpContext.Current.Server.MapPath("~/EmailFormats/AccountCreated.html");

            var apiKey = ConfigurationManager.AppSettings["emailServiceAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", ToName);
            var to = new EmailAddress(ToEmail, ToName);
            var plainTextContent = "Your new account has been created";
            //var htmlContent = "<strong>Your new account has been created</strong>";
            var htmlContent = AccountCreated;
            ListDictionary replacements = new ListDictionary();
            replacements.Add("<%Username%>", Username);
            var msg = MailHelper.CreateSingleEmail(from, to, Subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        //public static string GetHtmlBody(string EmailType)
        //{
        //    //using (StreamReader reader = File.OpenText(EmailType)) 
        //    //{                                                         
        //    //    return reader.ReadToEnd();
        //    //}
        //    //string html = "<strong>Your new account has been created</strong>";
        //    //return html;
        //}
    }
}