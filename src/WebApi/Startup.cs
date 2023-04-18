using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Nice.Eair.Observability;
using WebApi.Extensions;

namespace WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.RegisterServices();
        services.AddHealthChecks();
        services.AddControllers();
        services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
        app.UseObservability()
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            })

            // The readiness check uses all registered checks.
            .UseHealthChecks("/health/ready", new HealthCheckOptions
            {
                ResponseWriter = WriteResponse
            })

            // The liveness check ignores all registered checks and just comes back with OK
            .UseHealthChecks("/health/live", new HealthCheckOptions
            {
                // Exclude all checks and return a 200-Ok.
                Predicate = _ => false,
                ResponseWriter = WriteResponse
            });
    }

    private Task WriteResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var json = new { status = report.Status.ToString() };

        // k8s responds to liveness and readiness checks based on the status code.
        // The actual body is just for humans to read.
        // SUCCESS is indicated by 200 <= statusCode < 400
        if (report.Status != HealthStatus.Healthy)
        {
            // This will default to 200
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(json));
    }
}