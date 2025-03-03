using Application.Abstractions.Messaging;
using Application.DTOs;
using Application.DTOs.Post;

namespace Application.Posts.GetByUserId;
public sealed record GetPostsByUserIdQuery(int Page, int PageSize, string UserId) : IQuery<PaginatedByOffsetListDto<PostDto>>;
