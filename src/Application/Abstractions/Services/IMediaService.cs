using Application.DTOs.Media;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services;
public interface IMediaService
{
    Task<MediaUploadResultDto> UploadImageAsync(IFormFile file, string? publicId);
    Task<MediaUploadResultDto> UploadVideoAsync(IFormFile file, string? publicId);
    Task DeleteMediaAsync(string publicId);
}
