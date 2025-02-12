using Application.PetAlerts.Create;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PetAlerts;

public sealed class Create : IEndpoint
{
    private sealed record Request(Guid PetId, string PhoneNumber, string Note, DateTime LastSeen, string Address);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/pet-alerts/create", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreatePetAlertCommand(request.PetId, request.PhoneNumber, request.Note, request.LastSeen, request.Address);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        })
        .WithTags(Tags.PetAlert)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
