using Application.Advertisement.Update;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Advertisements;

public sealed class Update : IEndpoint
{
    private sealed record Request(Guid Id, string Title, Uri TargetUrl, double TotalCost, bool IsActive, Uri ImageUrl, string ImagePublicId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("v{version:apiVersion}/advertisements/update", [Authorize(Roles = $"{AppRoles.ADMIN},{AppRoles.SUPERADMIN}")] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateAdCommand(request.Id, request.Title, request.TargetUrl, request.TotalCost, request.IsActive, request.ImageUrl, request.ImagePublicId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Ad)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
