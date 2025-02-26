using Application.Abstractions.Messaging;

namespace Application.Comments.Update;
public sealed record UpdateCommentCommand(Guid Id, Guid PostId, string Content) : ICommand;
