using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs;
using Application.DTOs.Post;
using Application.DTOs.User;
using AutoMapper;
using Domain.Comments;
using Domain.Posts;
using Domain.Reactions;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using SharedKernel;

namespace Application.Posts.Get;
internal sealed class GetPostsQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICacheService cacheService,
    IUserContext userContext)
    : IQueryHandler<GetPostsQuery, PaginatedByOffsetListDto<PostDto>>
{
    public async Task<Result<PaginatedByOffsetListDto<PostDto>>> Handle(GetPostsQuery query, CancellationToken cancellationToken)
    {
        if (query.Page < 1 || query.PageSize < 1)
        {
            return Result.Failure<PaginatedByOffsetListDto<PostDto>>(CommonErrors.InvalidPaginationParameters(query.Page, query.PageSize));
        }

        string userId;
        try
        {
            userId = userContext.UserId;
        }
        catch { userId = string.Empty; }

        string cacheKey = string.IsNullOrEmpty(userId) ? $"posts:{query.Page}:{query.PageSize}" : $"posts:{userId}:{query.Page}:{query.PageSize}";
        string cacheData = await cacheService.GetCacheAsync(cacheKey);

        PaginatedByOffsetListDto<PostDto> result;
        if (!string.IsNullOrEmpty(cacheData))
        {
            result = JsonConvert.DeserializeObject<PaginatedByOffsetListDto<PostDto>>(cacheData);
            if (result is not null && result.Items is not null)
            {
                return Result.Success(result);
            }
        }

        IIncludableQueryable<Post, ApplicationUser> postsQuery = context.Posts
            .OrderByDescending(p => p.CreatedAt)
            .Include(p => p.User);

        int totalItems = await postsQuery.CountAsync(cancellationToken);
        if (totalItems == 0)
        {
            return Result.Success(new PaginatedByOffsetListDto<PostDto>());
        }

        int totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);
        if (query.Page > totalPages)
        {
            return Result.Failure<PaginatedByOffsetListDto<PostDto>>(CommonErrors.GreaterThanTotalPages(query.Page, totalPages));
        }

        List<Post> posts = await postsQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var postIds = posts.Select(p => p.Id).ToList();

        List<Reaction> reactions = await context.Reactions
            .Where(r => postIds.Contains(r.TargetId) && r.TargetType == ReactionTargetType.Post)
            .ToListAsync(cancellationToken);

        List<Comment> comments = await context.Comments
            .Where(c => postIds.Contains(c.PostId))
            .ToListAsync(cancellationToken);

        var postDtos = posts.Select(p => new PostDto
        {
            Id = p.Id,
            Caption = p.Caption,
            HashTag = p.HashTag,
            UpdatedAt = p.UpdatedAt,
            ImageUrl = p.ImageUrl,
            ImagePublicId = p.ImagePublicId,
            User = mapper.Map<UserShortInfoDto>(p.User),
            IsLiked = !string.IsNullOrEmpty(userId) && reactions.Any(r => r.TargetId == p.Id && r.UserId == userId),
            TotalReactions = reactions.Count(r => r.TargetId == p.Id),
            TotalComments = comments.Count(c => c.PostId == p.Id)
        }).ToList();

        result = new PaginatedByOffsetListDto<PostDto>(postDtos, totalItems, query.Page, query.PageSize);

        await cacheService.SetCacheAsync(cacheKey, result);

        return Result.Success(result);
    }
}
