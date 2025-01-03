using Application.Users.Register;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    public sealed record Request(string UserName, string Email, string FirstName, string LastName, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("api/v{version:apiVersion}/users/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.UserName,
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password);

            Result<string> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
