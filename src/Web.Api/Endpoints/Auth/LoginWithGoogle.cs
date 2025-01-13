using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;

namespace Web.Api.Endpoints.Auth;

public class LoginWithGoogle : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        //app.MapPost("v{version:apiVersion}/auth/login/refresh-token", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        //{
        //    var command = new LoginUserWithRefreshTokenCommand(request.RefreshToken);

        //    Result<LoginResponse> result = await sender.Send(command, cancellationToken);

        //    return result.Match(Results.Ok, CustomResults.Problem);
        //})
        //.WithTags(Tags.Auth)
        //.WithApiVersionSet(apiVersionSet)
        //.MapToApiVersion(new ApiVersion(2, 0));
    }
}
