using Application.Abstractions.Messaging;
using Application.DTOs.Post;

namespace Application.Posts.GetById;
public sealed record GetPostByIdQuery(Guid PostId) : IQuery<PostDto>;
