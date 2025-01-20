using SharedKernel;

namespace Domain.Auths;

public static class AuthErrors
{
    public static Error ProblemWhenLoginWithThirdParty(string provider) => Error.Problem(
        "Users.InvalidAccessToken",
        $"Something went wrong when we try to login using ${provider} account.");

    public static readonly Error InvalidAccessToken = Error.Problem(
        "Users.InvalidAccessToken",
        "The access token is invalid.");
}
