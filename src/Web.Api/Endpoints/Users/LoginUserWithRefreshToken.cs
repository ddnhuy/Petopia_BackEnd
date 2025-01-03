using Application.Users.Login;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class LoginUserWithRefreshToken : IEndpoint
{
    public sealed record Request(string RefreshToken);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("api/v{version:apiVersion}/users/refresh-token", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new LoginUserWithRefreshTokenCommand(request.RefreshToken);

            Result<LoginResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
