using Application.Abstractions.Messaging;

namespace Application.PetWeights.Create;
public sealed record CreatePetWeightCommand(Guid PetId, double Value) : ICommand;
