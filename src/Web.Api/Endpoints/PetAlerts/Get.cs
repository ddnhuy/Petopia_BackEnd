using Application.PetAlerts.Get;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.PetAlerts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PetAlerts;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/pet-alerts", async ([FromQuery] int page, [FromQuery] int pageSize, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetPetAlertsQuery(page, pageSize);

            Result<(List<PetAlert> petList, int totalPages, int totalItems)> result = await sender.Send(query, cancellationToken);

            return result.Match(
                success => Results.Ok(new
                {
                    TotalPages = success.totalPages,
                    TotalItems = success.totalItems,
                    Items = success.petList
                }), CustomResults.Problem);
        })
        .WithTags(Tags.PetAlert)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
