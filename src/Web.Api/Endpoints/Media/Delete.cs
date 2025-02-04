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
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapDelete("v{version:apiVersion}/media/delete/{publicId}", [Authorize] async (string publicId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteMediaCommand(publicId);
            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Media)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
