using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Domain;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Application.Users.Delete;
internal sealed class DeleteUserCommandHandler(UserManager<ApplicationUser> userManager, IUserContext userContext, IPublisher publisher)
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

        if (user.ImagePublicId is not null)
        {
            await publisher.Publish(new DeleteEntityHasImageDomainEvent(user.ImagePublicId), cancellationToken);
        }
        await userManager.DeleteAsync(user);

        return Result.Success();
    }
}
