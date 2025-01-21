using SharedKernel;

namespace Domain.Pets;
public static class PetErrors
{
    public static Error PetNotFound => Error.Problem(
        "Users.PetNotFound",
        "Pet not found.");

    public static Error PetAlreadyExists => Error.Problem(
        "Users.PetAlreadyExists",
        "Pet already exists.");

    public static Error PetTypeNotFound => Error.Problem(
        "Users.PetTypeNotFound",
        "Pet type not found.");

    public static Error InvalidPetBirthDate => Error.Problem(
        "Users.InvalidPetBirthDate",
        "Birth date must be less than or equal to the current date.");

    public static Error InvalidPetDeathDate => Error.Problem(
        "Users.InvalidPetDeathDate",
        "Death date must be greater than the birth date.");

    public static Error WeightNotFound => Error.Problem(
        "Users.WeightNotFound",
        "Weight not found.");

    public static Error InvalidPetWeightValue => Error.Problem(
        "Users.InvalidPetWeightValue",
        "Weight value must be greater than zero.");

    public static Error InvalidPetWeightDate => Error.Problem(
        "Users.InvalidPetWeightDate",
        "Weight date must be less than or equal to the current date.");

    public static Error PetVaccinationNotFound => Error.Problem(
        "Users.PetVaccinationNotFound",
        "Pet vaccination not found.");

    public static Error InvalidPetVaccinationDate => Error.Problem(
        "Users.InvalidPetVaccinationDate",
        "Vaccination date must be less than or equal to the current date.");

    public static Error InvalidPetVaccinationFrequency => Error.Problem(
        "Users.InvalidPetVaccinationFrequency",
        "Invalid vaccination frequency.");

    public static Error InvalidPetVaccinationName => Error.Problem(
        "Users.InvalidPetVaccinationName",
        "Vaccination name must not be empty.");
}
