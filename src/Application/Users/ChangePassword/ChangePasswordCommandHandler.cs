using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Application.Users.ChangePassword;
internal class ChangePasswordCommandHandler(
    IUserContext userContext,
    UserManager<ApplicationUser> userManager) : ICommandHandler<ChangePasswordCommand>
{
    public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userContext.UserId))
        {
            return Result.Failure(UserErrors.Unauthorized);
        }

        ApplicationUser? user = await userManager.FindByIdAsync(userContext.UserId);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(userContext.UserId));
        }

        if (!await userManager.CheckPasswordAsync(user, command.CurrentPassword))
        {
            return Result.Failure(UserErrors.WrongPassword);
        }

        if (!userManager.PasswordValidators.All(v => v.ValidateAsync(userManager, user, command.NewPassword).Result.Succeeded))
        {
            return Result.Failure(UserErrors.PasswordRequiresDigit);
        }

        await userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);

        return Result.Success();
    }
}
