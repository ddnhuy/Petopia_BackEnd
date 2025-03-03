using Application.Abstractions.Messaging;
using Application.DTOs;
using Application.DTOs.Post;

namespace Application.Posts.Get;
public sealed record GetPostsQuery(int Page, int PageSize) : IQuery<PaginatedByOffsetListDto<PostDto>>;
