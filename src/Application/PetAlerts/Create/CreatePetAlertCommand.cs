using Application.Abstractions.Messaging;

namespace Application.PetAlerts.Create;
public sealed record CreatePetAlertCommand(Guid PetId, string PhoneNumber, string Note, DateTime LastSeen, string Address) : ICommand;
