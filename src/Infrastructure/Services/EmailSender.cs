using Application.Abstractions.Email;
using Application.Abstractions.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Services;

internal sealed class EmailSender(
    ILogger<EmailSender> logger,
    IConfiguration configuration) : IEmailSender
{
    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        IConfigurationSection emailSettings = configuration.GetSection("EmailSettings");
        string senderName = emailSettings["SenderName"];
        string senderEmail = emailSettings["SenderEmail"];
        string senderPassword = emailSettings["SenderPassword"];
        string smtpServer = emailSettings["SmtpServer"];

        _ = int.TryParse(emailSettings["SmtpPort"], out int smtpPort);

        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName, senderEmail));
        message.To.Add(MailboxAddress.Parse(emailMessage.To));
        message.Subject = emailMessage.Subject;
        message.Body = new TextPart(TextFormat.Html) { Text = emailMessage.Body };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(senderEmail, senderPassword);
            await client.SendAsync(message);

            logger.LogInformation("Send an email with subject '{Subject}' to '{Email}' successfully!", emailMessage.Subject, emailMessage.To);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
