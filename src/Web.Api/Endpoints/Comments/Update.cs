using Application.Comments.Update;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Comments;

public sealed class Update : IEndpoint
{
    private sealed record Request(Guid Id, Guid PostId, string Content);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("v{version:apiVersion}/comments/update", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateCommentCommand(request.Id, request.PostId, request.Content);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Comment)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
