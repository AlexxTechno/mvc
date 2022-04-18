using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace mvc.Services
{
    public class EmailService
    {
        private readonly string name = "Сайт cleal.ru Изделия из кожи ручной работы";
        private readonly string emailFrom = "job@cleal.ru";
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly string my_gmail = "ak227482@gmail.com";
        private readonly string my_pass = "kuygfli8yroliv23";

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(name, emailFrom));   // login@yandex.ru
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient()) 
            {
                await client.ConnectAsync(smtpServer, 465, true); // true - ssl 
                await client.AuthenticateAsync(my_gmail, my_pass);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}

