using Application.DTOs.Post;
using Application.Posts.Get;
using Application.Posts.GetByUserId;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Posts;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/posts", async ([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? userId, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<(List<PostDto> postList, int totalPages, int totalItems)> result = string.IsNullOrEmpty(userId)
                ? await sender.Send(new GetPostsQuery(page, pageSize), cancellationToken)
                : await sender.Send(new GetPostsByUserIdQuery(page, pageSize, userId), cancellationToken);

            return result.Match(
                success => Results.Ok(new
                {
                    TotalPages = success.totalPages,
                    TotalItems = success.totalItems,
                    Items = success.postList
                }), CustomResults.Problem);
        })
        .WithTags(Tags.Post)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
