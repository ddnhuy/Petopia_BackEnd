using Application.DTOs.Pet;
using Application.Pets.GetPetType;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Statics;

public sealed class PetTypes : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/pet-types", async (ISender sender, CancellationToken cancellationToken) =>
        {
            GetPetTypesQuery query = new();
            Result<List<PetTypeDto>> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Static)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
