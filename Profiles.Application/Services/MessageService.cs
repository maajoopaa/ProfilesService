using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Services
{
    public class MessageService
    {
        public static void SendMessageAsync(string email, string head, string body)
        {
            Task.Run(() =>
            {
                SmtpClient smtpClient = new SmtpClient("smtp.mail.ru")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("maqd@list.ru", "quPTnRJrfmW1b2PprUmW"),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("maqd@list.ru"),
                    Subject = head,
                    Body = body,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);
            });
        }

    }
}
