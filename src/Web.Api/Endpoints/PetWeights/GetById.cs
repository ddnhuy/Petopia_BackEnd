using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using Application.PetWeights.GetById;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;
using Domain.Pets;

namespace Web.Api.Endpoints.PetWeights;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/petweights/{petWeightId}", [Authorize] async (Guid petWeightId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetPetWeightByIdQuery(petWeightId);

            Result<PetWeight> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.PetWeight)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
