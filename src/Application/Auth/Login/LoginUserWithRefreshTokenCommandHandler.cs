using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auths;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Auth.Login;
internal sealed class LoginUserWithRefreshTokenCommandHandler(
    IApplicationDbContext context,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserWithRefreshTokenCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginUserWithRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == command.RefreshToken, cancellationToken);

        if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Result.Failure<LoginResponse>(UserErrors.InvalidRefreshToken);
        }

        string accessToken = tokenProvider.GenerateAccessToken(refreshToken.User);

        refreshToken.Token = tokenProvider.GenerateRefreshToken();
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new LoginResponse(accessToken, refreshToken.Token));
    }
}
