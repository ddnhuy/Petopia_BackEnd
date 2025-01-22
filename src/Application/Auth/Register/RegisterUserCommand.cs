using Application.Abstractions.Messaging;

namespace Application.Auth.Register;

public sealed record RegisterUserCommand(string UserName, string Email, string FirstName, string LastName, string Password)
    : ICommand;
