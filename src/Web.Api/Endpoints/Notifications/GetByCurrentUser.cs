using Application.DTOs.Notification;
using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;
using Application.Notifications.GetByCurrentUser;
using Web.Api.Infrastructure;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Notifications;

internal sealed class GetByCurrentUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/notifications/currrent-user", [Authorize] async (ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetNotificationsByCurrentUser();

            Result<List<NotificationDto>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Notification)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0))
        .RequireAuthorization();
    }
}
