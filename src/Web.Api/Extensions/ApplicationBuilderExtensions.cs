namespace Web.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        });

        return app;
    }
}
