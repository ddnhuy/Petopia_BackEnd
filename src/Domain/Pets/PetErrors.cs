using SharedKernel;

namespace Domain.Pets;
public static class PetErrors
{
    public static Error PetNotFound => Error.NotFound(
        "Pets.PetNotFound",
        "Pet not found.");

    public static Error PetNotOwned => Error.Problem(
        "Pets.PetNotOwned",
        "You do not own this pet.");

    public static Error PetAlreadyExists => Error.Problem(
        "Pets.PetAlreadyExists",
        "Pet already exists.");

    public static Error PetWeightNotFound => Error.NotFound(
        "Pets.PetWeightNotFound",
        "Pet's weight not found.");

    public static Error PetVaccinationNotFound => Error.NotFound(
        "Pets.PetVaccinationNotFound",
        "Pet vaccination not found.");
}
