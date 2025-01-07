using Application.Abstractions.Email;

namespace Application.Abstractions.Services;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage emailMessage);
}
