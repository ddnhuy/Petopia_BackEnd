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

    public static Error WeightNotFound => Error.Problem(
        "Pets.WeightNotFound",
        "Weight not found.");

    public static Error InvalidPetWeightValue => Error.Problem(
        "Pets.InvalidPetWeightValue",
        "Weight value must be greater than zero.");

    public static Error InvalidPetWeightDate => Error.Problem(
        "Pets.InvalidPetWeightDate",
        "Weight date must be less than or equal to the current date.");

    public static Error PetVaccinationNotFound => Error.Problem(
        "Pets.PetVaccinationNotFound",
        "Pet vaccination not found.");

    public static Error InvalidPetVaccinationDate => Error.Problem(
        "Pets.InvalidPetVaccinationDate",
        "Vaccination date must be less than or equal to the current date.");

    public static Error InvalidPetVaccinationFrequency => Error.Problem(
        "Pets.InvalidPetVaccinationFrequency",
        "Invalid vaccination frequency.");

    public static Error InvalidPetVaccinationName => Error.Problem(
        "Pets.InvalidPetVaccinationName",
        "Vaccination name must not be empty.");
}
