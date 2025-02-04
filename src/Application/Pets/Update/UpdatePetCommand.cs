using Application.Abstractions.Messaging;
using Domain.Pets;
using SharedKernel;

namespace Application.Pets.Update;
public sealed record UpdatePetCommand(Guid Id, PetType Type, string Name, Uri? ImageUrl, string Description, DateTime BirthDate, DateTime? DeathDate, Gender Gender, bool IsSterilized, string? ImagePublicId) : ICommand;
