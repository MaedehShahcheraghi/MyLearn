using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Senders
{
    public class SendEmail
    {
        public static Task Send(string email, string subject,string htmlMessage)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("maedeh.shahcheraghi1384@gmail.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("maedeh.shahcheraghi1384@gmail.com", "sqdc xnlw msgm vubj");
            //<< بجای
            // x
            // ها، پسوردی که خود گوگل برای اپلیکیشنتون ساخته رو میزارید
            SmtpServer.EnableSsl = true; // only for port 465
            SmtpServer.Send(mail);
            return Task.CompletedTask;
        }
    }
}
