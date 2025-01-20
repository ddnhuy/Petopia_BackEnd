using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Users.Delete;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Application.Users;
internal sealed class DeleteUserCommandHandler(UserManager<ApplicationUser> userManager, IUserContext userContext)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        if (command.UserId != userContext.UserId)
        {
            return Result.Failure(UserErrors.Unauthorized);
        }

        ApplicationUser? user = await userManager.FindByIdAsync(command.UserId);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(command.UserId));
        }

        if (!await userManager.CheckPasswordAsync(user, command.Password))
        {
            return Result.Failure(UserErrors.WrongPassword);
        }

        await userManager.DeleteAsync(user);

        return Result.Success();
    }
}
