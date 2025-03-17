using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(string userId) => Error.NotFound(
        "Users.NotFound",
        $"Người dùng với ID '{userId}' không được tìm thấy.");

    public static Error Unauthorized => Error.Problem(
        "Users.Unauthorized",
        "Bạn không có quyền thực hiện hành động này.");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        "Người dùng với email được chỉ định không được tìm thấy.");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "Tên người dùng hoặc email đã được sử dụng.");

    public static readonly Error InvalidRefreshToken = Error.Problem(
        "Users.InvalidRefreshToken",
        "Mã làm mới không hợp lệ.");

    public static readonly Error EmailNotConfirmed = Error.Problem(
        "Users.EmailNotConfirmed",
        "Email của bạn chưa được xác nhận.");

    public static readonly Error UserLockedOut = Error.Problem(
        "Users.UserLockedOut",
        "Người dùng đã bị khóa.");

    public static readonly Error InvalidConfirmToken = Error.Problem(
        "Users.InvalidConfirmToken",
        "Mã xác nhận không hợp lệ.");

    public static readonly Error WrongPassword = Error.Problem(
        "Users.WrongPassword",
        "Bạn đã nhập sai mật khẩu.");

    public static readonly Error PasswordRequiresDigit = Error.Problem(
        "Users.PasswordRequiresDigit",
        "Mật khẩu phải phải dài ít nhất 8 kí tự, và phải chứa ít nhất một chữ cái, một chữ số và một kí tự đặc biệt.");
}
