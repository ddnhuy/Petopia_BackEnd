using Application.Abstractions.Messaging;
using Domain.Pets;

namespace Application.PetVaccinations.GetById;
public sealed record GetPetVaccinationByIdQuery(Guid PetVaccinationId) : IQuery<PetVaccination>;
