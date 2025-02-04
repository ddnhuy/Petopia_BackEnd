using SharedKernel;

namespace Domain.Pets;
public static class PetErrors
{
    public static Error PetNotFound => Error.NotFound(
        "Pets.PetNotFound",
        "Không tìm thấy thú cưng.");

    public static Error PetNotOwned => Error.Problem(
        "Pets.PetNotOwned",
        "Bạn không sở hữu thú cưng này.");

    public static Error PetAlreadyExists => Error.Problem(
        "Pets.PetAlreadyExists",
        "Thú cưng đã tồn tại.");

    public static Error PetWeightNotFound => Error.NotFound(
        "Pets.PetWeightNotFound",
        "Không tìm thấy cân nặng của thú cưng.");

    public static Error PetVaccinationNotFound => Error.NotFound(
        "Pets.PetVaccinationNotFound",
        "Không tìm thấy vắc-xin cho thú cưng.");
}
