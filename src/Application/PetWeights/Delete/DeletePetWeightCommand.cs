using Application.Abstractions.Messaging;

namespace Application.PetWeights.Delete;
public sealed record DeletePetWeightCommand(Guid PetWeightId) : ICommand;
