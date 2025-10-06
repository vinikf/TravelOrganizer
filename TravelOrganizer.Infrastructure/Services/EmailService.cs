using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using TravelOrganizer.Application.Interfaces;
using MimeKit;

namespace TravelOrganizer.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _emailUser;
        private readonly string _emailPassword;
        private readonly string _templatesPath;

        public EmailService(IConfiguration configuration)
        {
            _emailUser = configuration["EmailSettings:User"];
            _emailPassword = configuration["EmailSettings:Password"];
            _templatesPath = Path.Combine(AppContext.BaseDirectory, "EmailTemplates");
        }

        public async Task SendEmailAsync(string to, string subject, string templateName, object model)
        {
            var templateFile = Path.Combine(_templatesPath, $"{templateName}.html");
            if (!File.Exists(templateFile))
                throw new FileNotFoundException($"Template {templateName}.html não encontrado.");

            var body = await File.ReadAllTextAsync(templateFile);

            foreach (var prop in model.GetType().GetProperties())
            {
                var placeholder = $"{{{{{prop.Name}}}}}";
                var value = prop.GetValue(model)?.ToString() ?? string.Empty;
                body = body.Replace(placeholder, value);
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Travel Organizer", _emailUser));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailUser, _emailPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
