using Application.DTOs;
using Application.Statics.GetAdEventTypes;
using Application.Statics.GetNotificationTypes;
using Application.Statics.GetPetTypes;
using Application.Statics.GetReactionTargetTypes;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Statics;

public class StaticTypes : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        RouteGroupBuilder group = app.MapGroup("v{version:apiVersion}/statics");
        group.WithTags(Tags.Static);
        group.WithApiVersionSet(apiVersionSet);
        group.MapToApiVersion(new ApiVersion(2, 0));

        group.MapGet("/notification-types", async (ISender sender, CancellationToken cancellationToken) =>
        {
            GetNotificationTypesQuery query = new();
            Result<List<StaticTypeDto>> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        });

        group.MapGet("/pet-types", async (ISender sender, CancellationToken cancellationToken) =>
        {
            GetPetTypesQuery query = new();
            Result<List<StaticTypeDto>> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        });

        group.MapGet("/reaction-target-types", async (ISender sender, CancellationToken cancellationToken) =>
        {
            GetReactionTargetTypesQuery query = new();
            Result<List<StaticTypeDto>> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        });

        group.MapGet("/ad-event-types", async (ISender sender, CancellationToken cancellationToken) =>
        {
            GetAdEventTypesQuery query = new();
            Result<List<StaticTypeDto>> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}
