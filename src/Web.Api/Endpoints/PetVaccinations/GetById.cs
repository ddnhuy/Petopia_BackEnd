using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using Application.PetVaccinations.GetById;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;
using Domain.Pets;
using Application.DTOs.Pet;

namespace Web.Api.Endpoints.PetVaccinations;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/pet-vaccinations/{petVaccinationId}", [Authorize] async (Guid petVaccinationId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetPetVaccinationByIdQuery(petVaccinationId);

            Result<PetVaccinationDto> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.PetVaccination)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
