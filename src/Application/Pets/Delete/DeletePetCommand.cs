using Application.Abstractions.Messaging;

namespace Application.Pets.Delete;
public sealed record DeletePetCommand(Guid PetId) : ICommand;
