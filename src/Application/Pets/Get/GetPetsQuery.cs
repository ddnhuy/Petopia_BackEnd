using Application.Abstractions.Messaging;
using Application.DTOs.Pet;

namespace Application.Pets.Get;
public sealed record GetPetsQuery(string UserId, int Page, int PageSize) : IQuery<(List<PetDto>, int, int)>;
