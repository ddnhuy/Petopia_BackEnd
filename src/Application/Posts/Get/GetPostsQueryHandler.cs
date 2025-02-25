using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs.Post;
using AutoMapper;
using Domain.Posts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel;

namespace Application.Posts.Get;
internal sealed class GetPostsQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICacheService cacheService)
    : IQueryHandler<GetPostsQuery, (List<PostDto>, int, int)>
{
    public async Task<Result<(List<PostDto>, int, int)>> Handle(GetPostsQuery query, CancellationToken cancellationToken)
    {
        string cacheKey = $"posts:{query.Page}:{query.PageSize}";
        string cacheData = await cacheService.GetCacheAsync(cacheKey);

        (List<PostDto> postList, int, int) result;
        if (!string.IsNullOrEmpty(cacheData))
        {
            result = JsonConvert.DeserializeObject<(List<PostDto>, int, int)>(cacheData);
            if (result.postList is not null)
            {
                return Result.Success(result);
            }
        }

        List<Post> posts = await context.Posts
            .Include(p => p.User)
            .ToListAsync(cancellationToken);

        if (query.Page < 1)
        {
            return Result.Failure<(List<PostDto>, int, int)>(CommonErrors.PageLessThanOne);
        }

        if (query.PageSize < 1)
        {
            return Result.Failure<(List<PostDto>, int, int)>(CommonErrors.InvalidPageSize);
        }

        int totalItems = posts.Count;
        if (totalItems == 0)
        {
            return Result.Success((new List<PostDto>(), 0, 0));
        }

        int totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);
        if (query.Page > totalPages)
        {
            return Result.Failure<(List<PostDto>, int, int)>(CommonErrors.GreaterThanTotalPages(query.Page, totalPages));
        }

        result = (mapper.Map<List<PostDto>>(
            posts.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)).ToList(),
            totalPages,
            totalItems);

        await cacheService.SetCacheAsync(cacheKey, result);

        return Result.Success(result);
    }
}
