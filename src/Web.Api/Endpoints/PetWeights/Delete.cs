using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using Application.PetWeights.Delete;
using SharedKernel;
using Microsoft.AspNetCore.Authorization;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PetWeights;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapDelete("v{version:apiVersion}/petweights/{petWeightId}", [Authorize] async (Guid petWeightId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeletePetWeightCommand(petWeightId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.PetWeight)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
