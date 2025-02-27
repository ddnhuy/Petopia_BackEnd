using SharedKernel;

namespace Domain.Reactions;
public sealed record ReactionCreatedDomainEvent(Reaction Reaction) : IDomainEvent;
