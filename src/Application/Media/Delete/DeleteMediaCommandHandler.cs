using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using SharedKernel;

namespace Application.Media.Delete;
internal class DeleteMediaCommandHandler(
    IMediaService mediaService) : ICommandHandler<DeleteMediaCommand>
{
    public async Task<Result> Handle(DeleteMediaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await mediaService.DeleteMediaAsync(command.publicId);
            return Result.Success();
        }
        catch
        {
            return Result.Failure(CommonErrors.InvalidFile);
        }
    }
}
