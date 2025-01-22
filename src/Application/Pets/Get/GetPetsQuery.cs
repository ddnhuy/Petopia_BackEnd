using Application.Abstractions.Messaging;
using Application.DTOs.Pet;

namespace Application.Pets.Get;
public sealed record GetPetsQuery(string UserId) : IQuery<List<PetDto>>;
