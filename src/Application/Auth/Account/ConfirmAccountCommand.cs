using Application.Abstractions.Messaging;

namespace Application.Auth.Account;

public sealed record ConfirmAccountCommand(string Email, string Token) : ICommand<string>;
