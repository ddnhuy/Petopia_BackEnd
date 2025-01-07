using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Application.Auth.Account;

internal sealed class ConfirmAccountCommandHandler(
    UserManager<ApplicationUser> userManager) : ICommandHandler<ConfirmAccountCommand, string>
{
    public async Task<Result<string>> Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        string token = request.Token.Replace(' ', '+'); // Will remove after have FE to call MapPost
        IdentityResult result = await userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
        {
            return Result.Failure<string>(UserErrors.InvalidConfirmToken);
        }

        return Result.Success("Your email has been confirmed.");
    }
}
