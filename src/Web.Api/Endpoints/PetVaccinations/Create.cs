using Application.PetVaccinations.Create;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Pets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PetVaccinations;

public class Create : IEndpoint
{
    private sealed record Request(Guid PetId, DateTime Date, string VaccineName, string? Description, VaccinationFrequency Frequency);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/pet-vaccinations/create", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreatePetVaccinationCommand(request.PetId, request.Date, request.VaccineName, request.Description, request.Frequency);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        })
        .WithTags(Tags.PetVaccination)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
