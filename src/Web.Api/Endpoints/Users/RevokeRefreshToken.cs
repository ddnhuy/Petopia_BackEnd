using System.Security.Claims;
using Application.Auth.Account;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

public class RevokeRefreshToken : IEndpoint
{
    private sealed record Request(string UserId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/users/revoke-refresh-token", async (Request request, HttpContext httpContext, ISender sender, CancellationToken cancellationToken) =>
        {
            if (!Guid.TryParse(request.UserId, out _))
            {
                return Results.BadRequest("Invalid user id.");
            }

            string userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (request.UserId != userId)
            {
                return Results.BadRequest("You are not authorized to revoke this refresh token.");
            }

            var command = new RevokeRefreshTokensCommand(request.UserId);

            Result<bool> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.User)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
