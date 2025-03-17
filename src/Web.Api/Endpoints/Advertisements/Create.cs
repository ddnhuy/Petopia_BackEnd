using Application.Advertisement.Create;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Advertisements;

public sealed class Create : IEndpoint
{
    private sealed record Request(string Title, Uri TargetUrl, double TotalCost, Uri ImageUrl, string ImagePublicId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/advertisements/create", [Authorize(Roles = $"{AppRoles.ADMIN},{AppRoles.SUPERADMIN}")] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateAdCommand(request.Title, request.TargetUrl, request.TotalCost, request.ImageUrl, request.ImagePublicId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        })
        .WithTags(Tags.Ad)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
