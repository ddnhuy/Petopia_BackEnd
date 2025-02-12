using SharedKernel;

namespace Domain.PetAlerts;
public static class PetAlertErrors
{
    public static Error PetNotFound => Error.NotFound(
        "PetAlerts.PetAlertNotFound",
        "Không tìm thấy thông tin.");
}
