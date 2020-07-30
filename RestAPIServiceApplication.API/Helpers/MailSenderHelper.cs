using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace RestAPIServiceApplication.API.Helpers
{
    public class MailSenderHelper
    {
        public static async Task SendEmail(string receiver, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(receiver));
            message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient()) // Simple mail transfer protocol
            {
                var credential = new NetworkCredential
                {
                    UserName = WebConfigurationManager.AppSettings["AdminUser"],
                    Password = WebConfigurationManager.AppSettings["AdminPassWord"]
                };

                smtpClient.Credentials = credential;
                smtpClient.Host = WebConfigurationManager.AppSettings["SMTPName"];
                smtpClient.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}