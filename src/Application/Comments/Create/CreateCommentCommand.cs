using Application.Abstractions.Messaging;

namespace Application.Comments.Create;
public sealed record CreateCommentCommand(Guid PostId, string Content) : ICommand;
