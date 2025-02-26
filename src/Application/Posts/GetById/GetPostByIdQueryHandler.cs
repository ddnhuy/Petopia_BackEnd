using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs.Post;
using AutoMapper;
using Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel;

namespace Application.Posts.GetById;
internal sealed class GetPostByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICacheService cacheService)
    : IQueryHandler<GetPostByIdQuery, PostDto>
{
    public async Task<Result<PostDto>> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
    {
        string cacheKey = $"post:{query.PostId}";
        string cacheData = await cacheService.GetCacheAsync(cacheKey);

        PostDto result;
        if (!string.IsNullOrEmpty(cacheData))
        {
            result = JsonConvert.DeserializeObject<PostDto>(cacheData);
            if (result is not null)
            {
                return Result.Success(result);
            }
        }

        Post? post = await context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == query.PostId, cancellationToken);

        if (post is null)
        {
            return Result.Failure<PostDto>(PostErrors.PostNotFound);
        }

        result = mapper.Map<PostDto>(post);

        await cacheService.SetCacheAsync(cacheKey, result);

        return Result.Success(result);
    }
}
