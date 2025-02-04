using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs.Media;
using SharedKernel;

namespace Application.Media.Images.Upload;
internal class UploadImageCommandHandler(
    IMediaService mediaService) : ICommandHandler<UploadImageCommand, MediaUploadResultDto>
{
    public async Task<Result<MediaUploadResultDto>> Handle(UploadImageCommand command, CancellationToken cancellationToken)
    {
        try
        {
            MediaUploadResultDto uploadResult = await mediaService.UploadImageAsync(command.File, command.publicId);
            return Result.Success(uploadResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<MediaUploadResultDto>(CommonErrors.InvalidFile(ex.Message));
        }
    }
}
