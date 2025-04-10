using greenswamp.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace greenswamp.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpSettings _smtpSettings;


        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
            _smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                EnableSsl = _smtpSettings.EnableSsl,
                Credentials = new System.Net.NetworkCredential(_smtpSettings.Email, _smtpSettings.Password)
            };
        }

        public void SendEmail(string toEmail, string subject, string body, bool isBodyHtml = true)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            };
            mailMessage.To.Add(toEmail);

            _smtpClient.Send(mailMessage);
        }
    }
}