using Application.Abstractions.Messaging;
using Application.DTOs.Media;
using Microsoft.AspNetCore.Http;

namespace Application.Media.Images.Upload;
public sealed record UploadImageCommand(IFormFile File, string? publicId) : ICommand<MediaUploadResultDto>;
