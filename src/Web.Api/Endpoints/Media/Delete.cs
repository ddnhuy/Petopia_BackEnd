using Application.Media.Delete;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Media;

public sealed class Delete : IEndpoint
{
    private sealed record Request(string PublicId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/media/delete", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteMediaCommand(request.PublicId);
            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Media)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
