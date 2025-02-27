using SharedKernel;

namespace Domain.Comments;
public sealed record CommentCreatedDomainEvent(Comment Comment) : IDomainEvent;
