using Application.Abstractions.Messaging;
using Application.DTOs.Pet;

namespace Application.PetVaccinations.Get;
public sealed record GetPetVaccinationsQuery(Guid PetId) : IQuery<List<PetVaccinationDto>>;
