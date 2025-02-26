using Application.DTOs.Reaction;
using Application.Reactions.DeleteRange;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Reactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Reactions;

public sealed class DeleteRange : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/reactions/delete-range", [Authorize] async (List<ReactionDto> request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteRangeReactionCommand(request);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Reaction)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
