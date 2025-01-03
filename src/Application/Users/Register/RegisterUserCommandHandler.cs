using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IPublisher publisher)
    : ICommandHandler<RegisterUserCommand, string>
{
    public async Task<Result<string>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
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
            throw new ApplicationException($"Unable to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        if (!roleManager.RoleExistsAsync(AppRoles.SUPERADMIN).GetAwaiter().GetResult())
        {
            await roleManager.CreateAsync(new IdentityRole(AppRoles.SUPERADMIN));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.ADMIN));
        }
        if (!roleManager.RoleExistsAsync(AppRoles.USER).GetAwaiter().GetResult())
        {
            await roleManager.CreateAsync(new IdentityRole(AppRoles.USER));
        }

        ApplicationUser user = await context.Users.FirstAsync(u => u.UserName == newUser.UserName, cancellationToken);

        IdentityRole? userRole = await roleManager.FindByNameAsync(AppRoles.USER);
        if (userRole is not null && !string.IsNullOrEmpty(userRole.Name))
        {
            await userManager.AddToRoleAsync(user, userRole.Name);
        }

        await publisher.Publish(new UserRegisteredDomainEvent(user.Id), cancellationToken);

        return user.Id;
    }
}
