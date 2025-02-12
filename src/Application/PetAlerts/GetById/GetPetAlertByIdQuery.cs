using Application.Abstractions.Messaging;
using Domain.PetAlerts;

namespace Application.PetAlerts.GetById;
public sealed record GetPetAlertByIdQuery(Guid Id) : IQuery<PetAlert>;
