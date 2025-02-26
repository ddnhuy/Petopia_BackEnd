using Application.Posts.Create;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Posts;

public sealed class Create : IEndpoint
{
    private sealed record Request(string Caption, string HashTag, Uri ImageUrl, string ImagePublicId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/posts/create", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreatePostCommand(request.Caption, request.HashTag, request.ImageUrl, request.ImagePublicId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        })
        .WithTags(Tags.Post)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
