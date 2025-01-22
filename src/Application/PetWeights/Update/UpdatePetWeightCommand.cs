using Application.Abstractions.Messaging;

namespace Application.PetWeights.Update;
public sealed record UpdatePetWeightCommand(Guid PetWeightId, double Value) : ICommand;
