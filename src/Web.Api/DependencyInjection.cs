using Application.DTOs.Post;
using Asp.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Web.Api.Infrastructure;

namespace Web.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // REMARK: If you want to use Controllers, you'll need this.
        // Configure OData model
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntitySet<PostDto>("Posts");
        IEdmModel model = modelBuilder.GetEdmModel();

        services.AddSingleton(model);
        services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", model).Select().Filter().OrderBy().Count().Expand());

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
