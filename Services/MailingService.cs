using System;
using System.IO;
using System.Threading.Tasks;
using kms.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace kms.Services
{
    public class MailingService : IMailingService
    {
        private readonly IConfiguration _config;
        public MailingService(IConfiguration config)
        {
            this._config = config;
        }

        public async Task SendAsync(string email, string subject, string message)
        {
            var mailingConfig = _config.GetSection("mailing");
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Knowledge management system", mailingConfig["email"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(mailingConfig["server"], 465, true);
                await client.AuthenticateAsync(mailingConfig["email"], mailingConfig["password"]);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
