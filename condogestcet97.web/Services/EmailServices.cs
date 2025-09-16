using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace condogestcet97.web.Services
{
    public class EmailServices : IEmailServices, IEmailSender
    {
        private readonly EmailSettings _settings;

        public EmailServices(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        // This is my custom interface method to send personalized emails from my application
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var message = new MailMessage(_settings.From, to, subject, body)
            {
                IsBodyHtml = true
            };

            using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
            {
                Credentials = new NetworkCredential(_settings.SmtpUser, _settings.SmtpPass),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }

        // This is the Identity interface method, it is used for sending confirmation emails, password reset emails, etc.
        async Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
