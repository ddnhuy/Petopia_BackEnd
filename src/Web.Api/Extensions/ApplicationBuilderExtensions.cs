using Microsoft.OpenApi.Models;

namespace Web.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger(options => options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                string? prefix = httpReq.Headers["X-Forwarded-Prefix"].FirstOrDefault();
                if (!string.IsNullOrEmpty(prefix))
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{prefix}" }
                    };
                }
            }));

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("v2/swagger.json", "API V2");
            options.SwaggerEndpoint("v1/swagger.json", "API V1");
        });

        return app;
    }
}
