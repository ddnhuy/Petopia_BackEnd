using Application.Abstractions.Messaging;

namespace Application.Auth.Login;

public sealed record LoginUserCommand(string UserName, string Password) : ICommand<LoginResponse>;
