using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Post;
using AutoMapper;
using Domain.Posts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Posts.SearchByKeywords;
internal sealed class SearchPostsByKeywordHandler(IApplicationDbContext context,
    IMapper mapper) : IQueryHandler<SearchPostsByKeywordQuery, List<PostDto>>
{
    public async Task<Result<List<PostDto>>> Handle(SearchPostsByKeywordQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Post> posts = await Task.Run(() =>
        {
            return context.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt);
        }, cancellationToken);

        List<PostDto> postDtos = mapper.Map<List<PostDto>>(posts);

        return Result.Success(postDtos);
    }
}
