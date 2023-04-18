using Nice.Eair.Observability;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        //Environment.SetEnvironmentVariable("NEO_NO_FILTER", "TRUE");
        Environment.SetEnvironmentVariable("NEO_LOG_LEVEL", "DEBUG");
        CreateHostBuilder(args)
            .BuildAndStartWithMetrics();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseObservability()
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddEnvironmentVariables("APP_");
            })
            .ConfigureWebHostDefaults(builder =>
                builder.UseStartup<Startup>().UseKestrel());
    }
}