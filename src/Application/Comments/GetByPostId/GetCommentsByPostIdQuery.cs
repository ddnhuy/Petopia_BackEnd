using Application.Abstractions.Messaging;
using Application.DTOs.Comment;

namespace Application.Comments.GetByPostId;
public sealed record GetCommentsByPostIdQuery(Guid PostId) : IQuery<List<CommentDto>>;
