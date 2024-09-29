using Course.Application.UseCases.Permissions.GetUserPermissions;
using Course.Application.UseCases.Users.GenerateToken;
using Course.Application.UseCases.Users.Login;
using Course.Application.UseCases.Users.News;
using Course.Application.UseCases.Users.RefreshToken;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Application;

public static class Startup
{
    public static IServiceCollection AddWareouseApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<GetUserPermissionsUseCase>();
        services.AddScoped<ArticlesUseCase>();
        services.AddScoped<GenerateTokenUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<RefreshTokenUseCase>();

        
        return services;
    }
}