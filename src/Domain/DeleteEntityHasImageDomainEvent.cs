using SharedKernel;

namespace Domain;
public sealed record DeleteEntityHasImageDomainEvent(string ImagePublicId) : IDomainEvent;
