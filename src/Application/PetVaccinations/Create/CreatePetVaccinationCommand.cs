using Application.Abstractions.Messaging;
using Domain.Pets;

namespace Application.PetVaccinations.Create;
public sealed record CreatePetVaccinationCommand(Guid PetId, DateTime Date, string VaccineName, string? Description, VaccinationFrequency Frequency) : ICommand;
