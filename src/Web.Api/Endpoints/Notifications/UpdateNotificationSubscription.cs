using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using Application.Notifications.UpdateNotificationSubscription;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Web.Api.Infrastructure;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Notifications;

internal sealed class UpdateNotificationSubscription : IEndpoint
{
    private sealed record Request(string SubscriptionId);

    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("v{version:apiVersion}/notifications/update-subscription", [Authorize] async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateNotificationSubscriptionCommand(request.SubscriptionId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Notification)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0))
        .RequireAuthorization();
    }
}
