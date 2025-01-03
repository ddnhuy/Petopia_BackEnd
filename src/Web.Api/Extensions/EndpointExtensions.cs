using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Api.Endpoints;

namespace Web.Api.Extensions;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasDeprecatedApiVersion(new ApiVersion(1, 0))
            .HasApiVersion(new ApiVersion(2, 0))
            .HasApiVersion(new ApiVersion(3, 0))
            .ReportApiVersions()
            .Build();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder, apiVersionSet);
        }

        return app;
    }
}
