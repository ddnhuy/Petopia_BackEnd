using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auths;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Auth.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.UserName == command.UserName || u.Email == command.UserName, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail);
        }

        bool verified = await userManager.CheckPasswordAsync(user, command.Password);

        if (!verified)
        {
            return Result.Failure<LoginResponse>(UserErrors.WrongPassword);
        }

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            return Result.Failure<LoginResponse>(UserErrors.EmailNotConfirmed);
        }
        if (await userManager.IsLockedOutAsync(user))
        {
            return Result.Failure<LoginResponse>(UserErrors.UserLockedOut);
        }

        string accessToken = tokenProvider.GenerateAccessToken(user);

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = tokenProvider.GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
        };

        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success<LoginResponse>(new(accessToken, refreshToken.Token));
    }
}
