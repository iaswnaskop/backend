using backend.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;
using System.Net.Mail;

namespace backend.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly SmtSettings _smtpSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SmtSettings> smtpSettings, ILogger<EmailService> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
                {
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = _smtpSettings.EnableSsl,
                    Timeout = 10000 // 10 seconds timeout
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.From),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                Log.Information("Sending email to {To} via {Server}:{Port}", toEmail, _smtpSettings.Server, _smtpSettings.Port);

                await client.SendMailAsync(mailMessage);

                _logger.LogInformation("Email sent successfully to {toEmail}", toEmail);
            }
            catch (TaskCanceledException ex)
            {
                Log.Error(ex, "⏱ Timeout while sending email to {To}", toEmail);
                throw new Exception("Email send timeout — check your SMTP port or SSL settings.");
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP error sending email to {toEmail}", toEmail);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General error sending email to {toEmail}", toEmail);
                throw;
            }
        }

    }
}
