using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Application.Services
{
    public class EmailService
    {
        public EmailService() { }

        public async Task EnviarEmail(Usuario usuario, string destinatario, string assunto, string mensagem)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

                        I just wanted to let you know that Monica and I were going to go play some paintball, you in?

                        -- Joey"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("seu@email.com", "senha");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
