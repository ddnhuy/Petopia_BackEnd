using Application.Abstractions.Messaging;
using Domain.Pets;

namespace Application.PetVaccinations.Get;
public sealed record GetPetVaccinationsQuery(Guid PetId) : IQuery<List<PetVaccination>>;
