using Application.DTOs.Pet;
using Application.PetVaccinations.Get;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Pets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PetVaccinations;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/pet-vaccinations", [Authorize] async (Guid petId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetPetVaccinationsQuery(petId);

            Result<List<PetVaccinationDto>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.PetVaccination)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
