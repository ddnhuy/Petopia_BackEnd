using Application.Advertisement.Delete;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Advertisements;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapDelete("v{version:apiVersion}/advertisements/delete/{adId}", [Authorize(Roles = $"{AppRoles.ADMIN},{AppRoles.SUPERADMIN}")] async (Guid adId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteAdCommand(adId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Ad)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
