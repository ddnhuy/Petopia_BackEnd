using Application.Todos.Get;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Todos;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("v{version:apiVersion}/todos", async (string userId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetTodosQuery(userId);

            Result<List<TodoResponse>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Todo)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization();
    }
}
