using Application.Abstractions.Messaging;
using Application.DTOs.Pet;

namespace Application.PetWeights.GetById;
public sealed record GetPetWeightByIdQuery(Guid PetWeightId) : IQuery<PetWeightDto>;
