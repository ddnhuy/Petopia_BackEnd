using Application.Abstractions.Messaging;

namespace Application.Users.Delete;
public sealed record DeleteUserCommand(string UserId, string Password) : ICommand;
