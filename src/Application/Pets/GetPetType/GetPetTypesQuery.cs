using Application.Abstractions.Messaging;
using Application.DTOs.Pet;

namespace Application.Pets.GetPetType;
public sealed record GetPetTypesQuery() : IQuery<List<PetTypeDto>>;
