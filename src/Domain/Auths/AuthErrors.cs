using SharedKernel;

namespace Domain.Auths;

public static class AuthErrors
{
    public static Error ProblemWhenLoginWithThirdParty(string provider) => Error.Problem(
        "Users.InvalidAccessToken",
        $"Có lỗi xảy ra khi chúng tôi thử đăng nhập bằng tài khoản ${provider}.");

    public static readonly Error InvalidAccessToken = Error.Problem(
        "Users.InvalidAccessToken",
        "Mã thông báo truy cập không hợp lệ.");
}
