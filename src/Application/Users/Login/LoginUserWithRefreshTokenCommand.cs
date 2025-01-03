using Application.Abstractions.Messaging;

namespace Application.Users.Login;

public sealed record LoginUserWithRefreshTokenCommand(string RefreshToken) : ICommand<LoginResponse>;
