using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Auth.Login;

public sealed record LoginWithThirdPartyCommand(string Token, string LoginProvider) : ICommand<LoginResponse>;
