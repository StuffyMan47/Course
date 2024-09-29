using Course.Application.Services;
using Course.Application.Services.AppSettings;
using Course.Application.Services.UserContext;
using Course.Infrastructure.Authentication;
using Course.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddWareouseInfrastructureLayer(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        // services.AddWarehouseDataAccessLayer(config, environment);
        services.AddStoreWebAuth(config);

        // services.AddRequestLogging();
        // services.AddExceptionMiddleware();

        services.AddSingleton<IAppSettings, AppSettigns>();

        services.AddMemoryCache();

        services.AddScoped<IUserContextProvider, UserContextProvider>();
        services.AddServices();

        return services;
    }

    public static IApplicationBuilder UseWareouseInfrastructureLayer(this IApplicationBuilder app, IConfiguration config, IWebHostEnvironment environment)
    {
        // app.UseCorsPolicy();

        app.UseStoreWebAuth();
        // app.UseRequestLogging();
        // app.UseExceptionMiddleware();
        
        return app;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddServicesByInterface(typeof(ITransientService), ServiceLifetime.Transient)
            .AddServicesByInterface(typeof(IScopedService), ServiceLifetime.Scoped);

    private static IServiceCollection AddServicesByInterface(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime)
    {
        var interfaceTypes =
            AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName == null || !x.FullName.StartsWith("Course.Tests"))
                .SelectMany(s => s.GetTypes())
                .Where(t => interfaceType.IsAssignableFrom(t)
                    && t is { IsClass: true, IsAbstract: false })
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service is not null
                    && interfaceType.IsAssignableFrom(t.Service));

        foreach (var type in interfaceTypes)
        {
            services.AddServiceByInterface(type.Service!, type.Implementation, lifetime);
        }

        return services;
    }

    private static IServiceCollection AddServiceByInterface(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime) =>
        lifetime switch
        {
            ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
            ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
            ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
            _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
        };
}
