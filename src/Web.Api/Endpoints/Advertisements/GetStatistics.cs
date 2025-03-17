using Application.Advertisement.GetStatistics;
using Application.DTOs.Advertisement;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Advertisements;

public sealed class GetStatistics : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/advertisements/get-statistics", [Authorize(Roles = $"{AppRoles.ADMIN},{AppRoles.SUPERADMIN}")] async ([FromQuery] bool? IsActiveOnly, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetAdStatisticsQuery(IsActiveOnly ?? false);

            Result<IEnumerable<AdStatisticsDto>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Ad)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
