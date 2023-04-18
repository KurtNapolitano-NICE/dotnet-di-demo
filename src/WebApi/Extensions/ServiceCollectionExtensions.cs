using WebApi.Services;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<INextValueService, NextNextValueService>();

        services.AddHostedService<StartupHostedService>();
        services.AddHostedService<AlwaysOnBackgroundService>();

        return services;
    }
}