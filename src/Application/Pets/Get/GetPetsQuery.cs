using Application.Abstractions.Messaging;
using Application.DTOs;
using Application.DTOs.Pet;
using Application.DTOs.Post;

namespace Application.Pets.Get;
public sealed record GetPetsQuery(string UserId, int Page, int PageSize) : IQuery<PaginatedByOffsetListDto<PetDto>>;
