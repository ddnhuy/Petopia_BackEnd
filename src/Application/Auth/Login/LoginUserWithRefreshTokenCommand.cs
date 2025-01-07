using Application.Abstractions.Messaging;

namespace Application.Auth.Login;

public sealed record LoginUserWithRefreshTokenCommand(string RefreshToken) : ICommand<LoginResponse>;
