using Asp.Versioning.Builder;

namespace Web.Api.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app, ApiVersionSet apiVersionSet);
}
