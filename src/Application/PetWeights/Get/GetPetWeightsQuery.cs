using Application.Abstractions.Messaging;
using Domain.Pets;

namespace Application.PetWeights.Get;
public sealed record GetPetWeightsQuery(Guid PetId) : IQuery<List<PetWeight>>;
