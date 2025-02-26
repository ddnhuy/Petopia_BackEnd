using Application.DTOs.Post;
using Application.Posts.Get;
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
        app.MapGet("v{version:apiVersion}/posts", async ([FromQuery] int page, [FromQuery] int pageSize, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetPostsQuery(page, pageSize);

            Result<(List<PostDto> postList, int totalPages, int totalItems)> result = await sender.Send(query, cancellationToken);

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
