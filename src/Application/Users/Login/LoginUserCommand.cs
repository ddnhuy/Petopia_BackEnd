using Application.Abstractions.Messaging;

namespace Application.Users.Login;

public sealed record LoginUserCommand(string UserName, string Password) : ICommand<LoginResponse>;
