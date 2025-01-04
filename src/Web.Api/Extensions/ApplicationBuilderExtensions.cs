﻿namespace Web.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("v2/swagger.json", "API V2");
            options.SwaggerEndpoint("v1/swagger.json", "API V1");
        });

        return app;
    }
}
