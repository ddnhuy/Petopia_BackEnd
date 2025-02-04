using Application.DTOs.Media;
using Application.Media.Images.Upload;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Media;

public sealed class UploadImage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPost("v{version:apiVersion}/media/upload-image", [Authorize][Consumes("multipart/form-data")] async (HttpContext context, ISender sender, CancellationToken cancellationToken) =>
        {
            IFormCollection form = await context.Request.ReadFormAsync(cancellationToken);
            IFormFile file = form.Files.Count > 0 ? form.Files["image"] : null;

            if (file is null)
            {
                return Results.BadRequest("Không có ảnh nào được tải lên.");
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                return Results.BadRequest("Kích thước tệp tải lên không được vượt quá 10MB.");
            }

            string? publicId = form["publicId"].ToString();

            var command = new UploadImageCommand(file, publicId);
            Result<MediaUploadResultDto> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Media)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
