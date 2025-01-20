using SharedKernel;

namespace Domain.Auths;

public sealed record UserRegisteredDomainEvent(string UserId) : IDomainEvent;
