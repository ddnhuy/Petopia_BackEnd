using Application.Abstractions.Messaging;
using Domain.Pets;
using SharedKernel;

namespace Application.Pets.Create;
public sealed record CreatePetCommand(PetType Type, string Name, Uri? ImageUrl, string Description, DateTime BirthDate, Gender Gender, bool IsSterilized, string? ImagePublicId) : ICommand;
