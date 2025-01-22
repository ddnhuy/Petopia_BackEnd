using Application.Abstractions.Messaging;

namespace Application.PetVaccinations.Delete;
public sealed record DeletePetVaccinationCommand(Guid PetVaccinationId) : ICommand;
