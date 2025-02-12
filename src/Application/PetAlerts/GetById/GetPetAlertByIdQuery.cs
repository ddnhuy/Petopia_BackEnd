using Application.Abstractions.Messaging;
using Application.DTOs.PetAlert;

namespace Application.PetAlerts.GetById;
public sealed record GetPetAlertByIdQuery(Guid Id) : IQuery<PetAlertDto>;
