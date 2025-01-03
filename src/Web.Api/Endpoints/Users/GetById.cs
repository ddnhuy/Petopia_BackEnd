using Application.Users.GetById;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("api/v{version:apiVersion}/users/{userId}", [Authorize(Roles = AppRoles.USER)] async (string userId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetUserByIdQuery(userId);

            Result<UserResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
