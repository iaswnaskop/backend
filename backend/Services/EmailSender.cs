using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender<IdentityUser>
{
    private readonly SmtpClient _smtpClient;

    public EmailSender()
    {
        _smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 465,
            Credentials = new NetworkCredential("iaswnas.kop@gmail.com", "sbho owtw ppsd osoe"),
            EnableSsl = true,
        };
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress("iaswnas.kop@gmail.com"),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        return _smtpClient.SendMailAsync(mailMessage);
    }

    // ✅ ΜΕΘΟΔΟΣ ΓΙΑ CONFIRMATION LINK
    public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {
        string subject = "Confirm your email";
        string htmlMessage = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.";

        return SendEmailAsync(email, subject, htmlMessage);
    }

    // ✅ ΜΕΘΟΔΟΣ ΓΙΑ PASSWORD RESET LINK
    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        string subject = "Reset your password";
        string htmlMessage = $"You can reset your password by clicking <a href='{resetLink}'>here</a>.";

        return SendEmailAsync(email, subject, htmlMessage);
    }

    // ✅ ΝΕΑ ΜΕΘΟΔΟΣ ΓΙΑ PASSWORD RESET CODE
    public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        string subject = "Your password reset code";
        string htmlMessage = $"Your password reset code is: <strong>{resetCode}</strong>";

        return SendEmailAsync(email, subject, htmlMessage);
    }
}
