using Application.Todos.Complete;
using Application.Todos.Delete;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Todos;

internal sealed class Complete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapPut("api/v{version:apiVersion}/todos/{id:guid}/complete", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CompleteTodoCommand(id);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Todos)
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization();
    }
}
