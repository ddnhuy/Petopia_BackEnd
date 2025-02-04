using System.Globalization;
using Application.Abstractions.Services;
using Application.DTOs.Media;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;
internal sealed class MediaService(
    Cloudinary cloudinary) : IMediaService
{
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };

    public async Task<MediaUploadResultDto> UploadImageAsync(IFormFile file, string? publicId)
    {
        string extension = Path.GetExtension(file.FileName).ToLower(CultureInfo.CurrentCulture);
        if (!AllowedExtensions.Contains(extension))
        {
            throw new ArgumentException("Định dạng ảnh không hợp lệ. Chỉ được phép sử dụng JPG, PNG hoặc JPEG.");
        }

        await using Stream stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = publicId ?? Guid.NewGuid().ToString(),
            Folder = "petopia",
            UniqueFilename = false,
            Overwrite = true
        };

        ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams);

        return new MediaUploadResultDto
        {
            Url = uploadResult.SecureUrl.AbsoluteUri,
            PublicId = uploadResult.PublicId.Substring(uploadResult.PublicId.LastIndexOf('/') + 1)
        };
    }

    public Task<MediaUploadResultDto> UploadVideoAsync(IFormFile file, string? publicId)
    {
        throw new NotImplementedException("Chức năng chưa hỗ trợ.");
    }

    public async Task<bool> DeleteMediaAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        DeletionResult deleteResult = await cloudinary.DestroyAsync(deleteParams);

        return deleteResult.Result == "ok";
    }
}
