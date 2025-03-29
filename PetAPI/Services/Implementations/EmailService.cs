using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using PetAPI.Services.Interfaces;

namespace PetAPI.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationEmailAsync(string email, string code)
        {
            var smtpServer = _configuration["Email:SmtpServer"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"]);
            var smtpUsername = _configuration["Email:Username"];
            var smtpPassword = _configuration["Email:Password"];
            var fromEmail = _configuration["Email:FromEmail"];

            var message = new MailMessage
            {
                From = new MailAddress(email),
                Subject = "Activate your account",
                Body = $"<p>Your verification code is: <strong>{code}</strong></p>",
                IsBodyHtml = true
            };
            message.To.Add(email);

            using (var client = new SmtpClient(smtpServer, smtpPort)) 
            {
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
        }
    }
}
