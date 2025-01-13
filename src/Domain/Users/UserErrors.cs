using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(string userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found.");

    public static Error Unauthorized() => Error.Problem(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        "The user with the specified email was not found.");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided username or email is not unique.");

    public static readonly Error InvalidRefreshToken = Error.Problem(
        "Users.InvalidRefreshToken",
        "The refesh token is invalid.");

    public static readonly Error EmailNotConfirmed = Error.Problem(
        "Users.EmailNotConfirmed",
        "Your email is not confirmed.");

    public static readonly Error UserLockedOut = Error.Problem(
        "Users.UserLockedOut",
        "The user was locked out.");

    public static readonly Error InvalidConfirmToken = Error.Problem(
        "Users.InvalidConfirmToken",
        "The confirmation token is invalid.");
}
