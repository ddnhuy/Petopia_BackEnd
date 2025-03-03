using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs.Comment;
using AutoMapper;
using Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel;

namespace Application.Comments.GetByPostId;
internal sealed class GetCommentsByPostIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICacheService cacheService)
    : IQueryHandler<GetCommentsByPostIdQuery, List<CommentDto>>
{
    public async Task<Result<List<CommentDto>>> Handle(GetCommentsByPostIdQuery query, CancellationToken cancellationToken)
    {
        string cacheKey = $"comment:post:{query.PostId}";
        string cacheData = await cacheService.GetCacheAsync(cacheKey);

        List<CommentDto> result;
        if (!string.IsNullOrEmpty(cacheData))
        {
            result = JsonConvert.DeserializeObject<List<CommentDto>>(cacheData);
            if (result is not null)
            {
                return Result.Success(result);
            }
        }

        List<Comment>? commentList = await context.Comments
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .Where(p => p.PostId == query.PostId)
            .ToListAsync(cancellationToken);

        if (commentList is null || commentList.Count == 0)
        {
            return Result.Success(new List<CommentDto>());
        }

        result = mapper.Map<List<CommentDto>>(commentList);

        await cacheService.SetCacheAsync(cacheKey, result);

        return Result.Success(result);
    }
}
