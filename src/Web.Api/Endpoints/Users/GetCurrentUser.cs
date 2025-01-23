using Asp.Versioning.Builder;
using Asp.Versioning;
using MediatR;
using Application.Abstractions.Authentication;
using Application.Users.GetById;
using Application.DTOs.User;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Endpoints.Users;

public sealed class GetCurrentUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/users/current-user", [Authorize] async (IUserContext userContext, ISender sender, CancellationToken cancellationToken) =>
        {
            string userId = userContext.UserId;
            var query = new GetUserByIdQuery(userId);

            Result<UserDto> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.User)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0));
    }
}
