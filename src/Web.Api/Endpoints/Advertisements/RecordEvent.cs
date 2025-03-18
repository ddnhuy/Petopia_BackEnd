using Application.Advertisement.RecordEvent;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Advertisement;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Advertisements;

public sealed class RecordEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        RouteGroupBuilder group = app.MapGroup("v{version:apiVersion}/advertisements");
        group.WithTags(Tags.Ad);
        group.WithApiVersionSet(apiVersionSet);
        group.MapToApiVersion(new ApiVersion(2, 0));

        group.MapPost("/{adId}/impression", async (Guid adId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RecordEventCommand(adId, AdEventType.Impression);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        });

        group.MapPost("/{adId}/click", async (Guid adId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RecordEventCommand(adId, AdEventType.Click);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        });
    }
}
