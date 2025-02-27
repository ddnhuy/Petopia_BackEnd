using Application.Notifications.Read;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Notifications;

internal sealed class Read : IEndpoint
{
    private sealed record Request(Guid NotificationId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("v{version:apiVersion}/notifications/read", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ReadNotificationCommand(request.NotificationId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Notification)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0))
        .RequireAuthorization();
    }
}
