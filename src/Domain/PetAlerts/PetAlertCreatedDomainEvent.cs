using SharedKernel;

namespace Domain.PetAlerts;
public sealed record PetAlertCreatedDomainEvent(PetAlert PetAlert) : IDomainEvent;
