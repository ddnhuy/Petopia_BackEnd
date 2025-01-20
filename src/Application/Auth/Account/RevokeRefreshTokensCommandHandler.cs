using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auths;
using SharedKernel;

namespace Application.Auth.Account;
internal sealed class RevokeRefreshTokensCommandHandler(
    IApplicationDbContext context) : ICommandHandler<RevokeRefreshTokensCommand, bool>
{
    public async Task<Result<bool>> Handle(RevokeRefreshTokensCommand command, CancellationToken cancellationToken)
    {
        await context.ExecuteDeleteAsync<RefreshToken>(x => x.UserId == command.UserId, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
