using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage(); //MimeKit package
            //configurare
            emailToSend.From.Add(MailboxAddress.Parse("hello@trainingDotNet.com"));//adresa de la care trimitem mail-ul
            emailToSend.To.Add(MailboxAddress.Parse(email));//adresa la care trimitem mail-ul
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage}; 


            //Sending email
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com",587,MailKit.Security.SecureSocketOptions.StartTls);//conectiune cu server-ul
                emailClient.Authenticate("ureche.eusebiu@gmail.com", "jenm hrrt puzy motr");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);

            }
            return Task.CompletedTask;
        }
    }
}
