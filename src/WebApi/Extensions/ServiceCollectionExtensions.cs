using Microsoft.Extensions.Options;
using WebApi.Services;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<NextValueService>();
        services.AddTransient<DbNextValueService>();

        services
            .AddTransient<ICalculationService, CalculationService>()
            .AddTransient<IDataRepository, DataRepository>();

        services.AddTransient<INextValueService>(provider =>
        {
            var useDatabase = bool.Parse(Environment.GetEnvironmentVariable("SHOULD_USE_DB"));

            if (useDatabase)
            {
                return provider.GetRequiredService<DbNextValueService>();
            }

            return provider.GetRequiredService<NextValueService>();
        });

        services.AddHostedService<AlwaysOnBackgroundService>();
        services.AddHostedService<StartupHostedService>();

        return services;
    }
}

// FACTORY CLASS = BAD
//public class NextValueSvcFactory
//{
//    public INextValueService GetService()
//    {
//        var useDatabase = bool.Parse(Environment.GetEnvironmentVariable("SHOULD_USE_DB"));
//        if (useDatabase)
//        {
//            return new DbNextValueService();
//        }
//        else
//        {
//            return new NextValueService(new HttpClient());
//        }
//    }
//}