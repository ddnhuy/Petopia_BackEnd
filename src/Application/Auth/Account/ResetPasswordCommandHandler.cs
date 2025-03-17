using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.Email;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Application.Auth.Account;
internal sealed class ResetPasswordCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailQueue emailQueue) : ICommandHandler<ResetPasswordCommand>
{
    public async Task<Result> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFoundByEmail);
        }

        string userIdBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id));
        byte[] randomBytes = new byte[4];
        RandomNumberGenerator.Fill(randomBytes);
        int randomNumber = BitConverter.ToInt32(randomBytes, 0);
        randomNumber = Math.Abs(randomNumber) % 10000;

        string newPassword = $"Petopia@{userIdBase64.Substring(0, 4)}{randomNumber}";

        await userManager.ResetPasswordAsync(user, await userManager.GeneratePasswordResetTokenAsync(user), newPassword);

        var emailMessage = new EmailMessage(
            command.Email,
            AppStrings.EmailSubject_ResetPassword,
            AppStrings.EmailContent_ResetPassword(newPassword));
        emailQueue.Enqueue(emailMessage);

        return Result.Success();
    }
}
