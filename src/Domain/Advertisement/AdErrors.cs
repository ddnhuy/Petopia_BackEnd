using SharedKernel;

namespace Domain.Advertisement;
public static class AdErrors
{
    public static Error AdNotFound => Error.NotFound(
        "Ads.AdNotFound",
        "Không tìm thấy thông tin về quảng cáo này.");

    public static Error AdNotHavePermission => Error.Problem(
        "Ads.AdNotHavePermission",
        "Bạn không có quyền thực hiện hành động này.");
}
