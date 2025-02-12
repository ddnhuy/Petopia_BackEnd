using Application.Abstractions.Messaging;
using Domain.PetAlerts;

namespace Application.PetAlerts.Get;
public sealed record GetPetAlertsQuery(int Page, int PageSize) : IQuery<(List<PetAlert>, int, int)>;
