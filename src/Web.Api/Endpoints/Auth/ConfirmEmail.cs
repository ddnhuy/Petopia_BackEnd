using Application.Auth.Account;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

internal sealed class ConfirmEmail : IEndpoint
{
    // Will use after have FE to call MapPost
    //private sealed record Request(string email, string token);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/auth/confirm-email", async ([FromQuery] string email, [FromQuery] string token, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ConfirmAccountCommand(email, token);

            Result<string> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Auth)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
