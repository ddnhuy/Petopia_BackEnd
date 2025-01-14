using Application.Auth.Login;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

public class LoginWithGoogle : IEndpoint
{
    private sealed record Request(string AccessToken);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/auth/login/google", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            if (string.IsNullOrEmpty(request.AccessToken))
            {
                return Results.BadRequest();
            }

            var command = new LoginWithThirdPartyCommand(request.AccessToken, LoginProvider.GOOGLE);

            Result<LoginResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Auth)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));

        app.MapPost("v{version:apiVersion}/auth/login/facebook", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            if (string.IsNullOrEmpty(request.AccessToken))
            {
                return Results.BadRequest();
            }

            var command = new LoginWithThirdPartyCommand(request.AccessToken, LoginProvider.FACEBOOK);

            Result<LoginResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Auth)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
