using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Gestao.Data;

namespace Gestao.Libraries.Email;

public class EmailSender(ILogger<EmailSender> logger) : IEmailSender<ApplicationUser>
{
    private readonly ILogger logger = logger;

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email,
        string confirmationLink) => SendEmailAsync(email, "Confirm your email",
        "Please confirm your account by " +
        $"<a href='{confirmationLink}'>clicking here</a>.");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email,
        string resetLink) => SendEmailAsync(email, "Reset your password",
        $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email,
        string resetCode) => SendEmailAsync(email, "Reset your password",
        $"Please reset your password using the following code: {resetCode}");

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        await Execute(subject, message, toEmail);
    }

    public async Task Execute(string subject, string message,
        string toEmail)
    {
        /*
          Substituir mandril pelo smtp - gmail
        var api = new MandrillApi(apiKey);
        var mandrillMessage = new MandrillMessage("sarah@contoso.com", toEmail,
            subject, message);
        await api.Messages.SendAsync(mandrillMessage);
        */
        logger.LogInformation("Email to {EmailAddress} sent!", toEmail);
    }
}