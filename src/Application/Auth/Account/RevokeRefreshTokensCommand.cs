using Application.Abstractions.Messaging;

namespace Application.Auth.Account;

public sealed record RevokeRefreshTokensCommand(string UserId) : ICommand<bool>;
