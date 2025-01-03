using Application.Abstractions.Messaging;

namespace Application.Users.Login;

public sealed record RevokeRefreshTokensCommand(string UserId) : ICommand<bool>;
