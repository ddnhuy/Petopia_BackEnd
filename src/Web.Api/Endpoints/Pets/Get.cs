using Application.DTOs;
using Application.DTOs.Pet;
using Application.Pets.Get;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Pets;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/pets", async ([FromQuery] string userId, [FromQuery] int page, [FromQuery] int pageSize, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetPetsQuery(userId, page, pageSize);

            Result<PaginatedByOffsetListDto<PetDto>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Pet)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
