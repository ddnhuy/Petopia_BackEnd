using Application.Pets.Create;
using Application.Pets.Update;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Pets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Pets;

public sealed class Update : IEndpoint
{
    private sealed record Request(Guid Id, PetType Type, string Name, Uri? ImageUrl, string Description, DateTime BirthDate, DateTime? DeathDate, Gender Gender, bool IsSterilized, string? ImagePublicId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("v{version:apiVersion}/pets/update", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdatePetCommand(
                request.Id,
                request.Type,
                request.Name,
                request.ImageUrl,
                request.Description,
                request.BirthDate,
                request.DeathDate,
                request.Gender,
                request.IsSterilized,
                request.ImagePublicId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Pet)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
