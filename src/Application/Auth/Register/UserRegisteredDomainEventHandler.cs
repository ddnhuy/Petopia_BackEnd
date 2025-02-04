using Application.Abstractions.Email;
using Domain.Auths;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SharedKernel;

namespace Application.Auth.Register;

internal sealed class UserRegisteredDomainEventHandler(
    IConfiguration configuration,
    UserManager<ApplicationUser> userManager,
    IEmailQueue emailQueue) : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        ApplicationUser user = await userManager.FindByIdAsync(notification.UserId) ?? throw new InvalidOperationException("Không tìm thấy người dùng.");

        if (string.IsNullOrEmpty(user.Email) || user.EmailConfirmed)
        {
            return;
        }

        string confirmToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

        IConfigurationSection serverInfo = configuration.GetSection("ServerInformation");
        string domain = serverInfo["Domain"];
        string confirmEmailLink = string.Empty;

        if (serverInfo["Environment"] == "Development")
        {
            string version = serverInfo["Version"];
            confirmEmailLink = $"{domain}/{version}/auth/confirm-email?email={user.Email}&token={confirmToken}";
        }
        else
        {
            confirmEmailLink = $"{domain}/email-comfirmation/?email={user.Email}&token={confirmToken}";
        }

        emailQueue.Enqueue(
            new EmailMessage(
                user.Email,
                AppStrings.EmailSubject_ConfirmAccount,
                AppStrings.EmailContent_ConfirmAccount(confirmEmailLink)));
    }
}
