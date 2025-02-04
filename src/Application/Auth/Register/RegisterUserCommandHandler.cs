using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auths;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Auth.Register;

internal sealed class RegisterUserCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IPublisher publisher)
    : ICommandHandler<RegisterUserCommand>
{
    public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email || u.UserName == command.UserName, cancellationToken))
        {
            return Result.Failure<string>(UserErrors.EmailNotUnique);
        }

        var newUser = new ApplicationUser
        {
            UserName = command.UserName,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        IdentityResult result = await userManager.CreateAsync(newUser, command.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException($"Không thể tạo người dùng: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        await userManager.AddToRoleAsync(newUser, AppRoles.USER);

        await publisher.Publish(new UserRegisteredDomainEvent(newUser.Id), cancellationToken);

        return Result.Success();
    }
}
