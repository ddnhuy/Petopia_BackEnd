using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Update;
internal sealed class UpdateUserCommandHandler(IApplicationDbContext context, IUserContext userContext, IMapper mapper)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (command.UserDTO.Id != userContext.UserId)
        {
            return Result.Failure(UserErrors.Unauthorized);
        }

        if (await context.Users.AnyAsync(u => u.Id != command.UserDTO.Id && (u.Email == command.UserDTO.Email || u.UserName == command.UserDTO.UserName), cancellationToken))
        {
            return Result.Failure<string>(UserErrors.EmailNotUnique);
        }

        ApplicationUser? user = await context.Users.FindAsync([command.UserDTO.Id], cancellationToken: cancellationToken);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(command.UserDTO.Id));
        }

        mapper.Map(command.UserDTO, user);

        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
