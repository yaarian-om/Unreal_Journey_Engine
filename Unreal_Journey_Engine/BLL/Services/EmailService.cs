using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailService
    {
        private const string SmtpHost = "smtp.gmail.com"; // SMTP Server
        private const int SmtpPort = 587; // or 465
        private const string SmtpUsername = "the.intersteller.library@gmail.com"; // Sender Email
        private const string SmtpPassword = "ylvfyjxlcjgbbduw"; // Auto Generated Password

        public static bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(SmtpHost, SmtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(SmtpUsername, SmtpPassword);
                    client.EnableSsl = true;

                    var message = new MailMessage
                    {
                        From = new MailAddress(SmtpUsername),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false
                    };

                    message.To.Add(toEmail);

                    client.Send(message);
                }
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }

}
