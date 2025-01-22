using Application.Abstractions.Messaging;
using Domain.Pets;

namespace Application.PetWeights.GetById;
public sealed record GetPetWeightByIdQuery(Guid PetWeightId) : IQuery<PetWeight>;
