using Application.Abstractions.Messaging;
using Domain.Pets;

namespace Application.PetVaccinations.Update;
public sealed record UpdatePetVaccinationCommand(Guid PetVaccinationId, DateTime Date, string VaccineName, string? Description, VaccinationFrequency Frequency) : ICommand;
