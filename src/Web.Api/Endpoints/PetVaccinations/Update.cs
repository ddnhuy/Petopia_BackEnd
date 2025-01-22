using Application.PetVaccinations.Update;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Pets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PetVaccinations;

public class Update : IEndpoint
{
    private sealed record Request(Guid PetVaccinationId, DateTime Date, string VaccineName, string? Description, VaccinationFrequency Frequency);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("v{version:apiVersion}/pet-vaccinations/update", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdatePetVaccinationCommand(request.PetVaccinationId, request.Date, request.VaccineName, request.Description, request.Frequency);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.PetVaccination)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
