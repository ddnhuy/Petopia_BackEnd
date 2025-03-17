using Application.Users.ChangePassword;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class ChangePassword : IEndpoint
{
    private sealed record Request(string CurrentPassword, string NewPassword);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("v{version:apiVersion}/users/change-password", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ChangePasswordCommand(request.CurrentPassword, request.NewPassword);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.User)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
