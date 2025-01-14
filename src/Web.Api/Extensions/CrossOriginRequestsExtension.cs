namespace Web.Api.Extensions;

public static class CrossOriginRequestsExtension
{
    public static void AddCORSPolicies(this IServiceCollection services)
    {
        services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
    }
}
