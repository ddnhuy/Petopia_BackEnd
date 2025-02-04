using Application.Abstractions.Services;
using Domain;
using MediatR;

namespace Application;
internal sealed class DeleteEntityHasImageDomainEventHandler(
    IMediaService mediaService) : INotificationHandler<DeleteEntityHasImageDomainEvent>
{
    public async Task Handle(DeleteEntityHasImageDomainEvent notification, CancellationToken cancellationToken)
    {
        await mediaService.DeleteMediaAsync(notification.ImagePublicId);
    }
}
