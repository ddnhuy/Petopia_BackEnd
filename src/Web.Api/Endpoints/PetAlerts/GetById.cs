using Application.PetAlerts.GetById;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.PetAlerts;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PetAlerts;

public sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/pet-alerts/{petAlertId}", async (Guid petAlertId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetPetAlertByIdQuery(petAlertId);

            Result<PetAlert> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.PetAlert)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
