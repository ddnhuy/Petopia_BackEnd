using SharedKernel;

namespace Domain.Notifications;

public static class NotificationErrors
{
    public static Error NotFound => Error.NotFound(
        "Notifications.NotFound",
        $"Không tìm thấy thông báo này.");
}
