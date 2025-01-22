using Application.Abstractions.Messaging;
using Application.DTOs.Pet;

namespace Application.Pets.GetById;
public sealed record GetPetByIdQuery(Guid PetId) : IQuery<PetDto>;
