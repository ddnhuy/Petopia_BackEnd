using Application.Abstractions.Messaging;
using Application.DTOs.Pet;

namespace Application.PetWeights.Get;
public sealed record GetPetWeightsQuery(Guid PetId) : IQuery<List<PetWeightDto>>;
