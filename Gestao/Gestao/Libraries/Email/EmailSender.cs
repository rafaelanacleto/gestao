using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Gestao.Data;

namespace Gestao.Libraries.Email;

public class EmailSender(ILogger<EmailSender> logger, IConfiguration configuration, SmtpClient smtp) : IEmailSender<ApplicationUser>
{
    private readonly ILogger logger = logger;
    private readonly SmtpClient smtp = smtp;
    private readonly IConfiguration configuration = configuration;

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email,
        string confirmationLink) => SendEmailAsync(email, "Confirme seu e-mail",
        "Por favor confirme sua conta ate " +
        $"<a href='{confirmationLink}'>clicando aqui</a>.");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email,
        string resetLink) => SendEmailAsync(email, "Reset sua senha",
        $"Por favor, redefina sua senha ate <a href='{resetLink}'>clicando aqui</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email,
        string resetCode) => SendEmailAsync(email, "Reset sua senha",
        $"Por favor, redefina sua senha usando o seguinte codigo: {resetCode}");

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        await Execute(subject, message, toEmail);
    }

    public async Task Execute(string subject, string message,
        string toEmail)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(configuration.GetValue<string>("EmailSender:User")!);
        mail.To.Add(new MailAddress(toEmail));
        mail.Subject = subject;
        mail.Body = message;
        mail.IsBodyHtml = true;

        await smtp.SendMailAsync(mail);
        logger.LogInformation("Email to {EmailAddress} enviado!", toEmail);
    }
}