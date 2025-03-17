using Application.Abstractions.Messaging;

namespace Application.Auth.Account;
public sealed record ResetPasswordCommand(string Email) : ICommand;
