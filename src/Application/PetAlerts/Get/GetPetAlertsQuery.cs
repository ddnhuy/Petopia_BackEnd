using Application.Abstractions.Messaging;
using Application.DTOs.PetAlert;

namespace Application.PetAlerts.Get;
public sealed record GetPetAlertsQuery(int Page, int PageSize) : IQuery<(List<PetAlertDto>, int, int)>;
