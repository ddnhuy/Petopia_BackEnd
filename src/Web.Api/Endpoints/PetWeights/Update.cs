using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using Application.PetWeights.Update;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Infrastructure;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.PetWeights;

public class Update : IEndpoint
{
    private sealed record Request(Guid PetWeightId, double Value);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("v{version:apiVersion}/pet-weights/update", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdatePetWeightCommand(request.PetWeightId, request.Value);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.PetWeight)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
