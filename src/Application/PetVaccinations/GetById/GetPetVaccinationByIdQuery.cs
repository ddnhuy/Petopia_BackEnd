using Application.Abstractions.Messaging;
using Application.DTOs.Pet;

namespace Application.PetVaccinations.GetById;
public sealed record GetPetVaccinationByIdQuery(Guid PetVaccinationId) : IQuery<PetVaccinationDto>;
