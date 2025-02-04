using Application.Pets.Create;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Pets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Pets;

public sealed class Create : IEndpoint
{
    private sealed record Request(PetType Type, string Name, Uri? ImageUrl, string Description, DateTime BirthDate, Gender Gender, bool IsSterilized, string? ImagePublicId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/pets/create", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreatePetCommand(
                request.Type,
                request.Name,
                request.ImageUrl,
                request.Description,
                request.BirthDate,
                request.Gender,
                request.IsSterilized,
                request.ImagePublicId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        })
        .WithTags(Tags.Pet)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
