using Course.Application.Exceptions;
using Course.Application.Services.AppSettings;
using Course.Application.Services.AppSettings.Models;
using Microsoft.Extensions.Configuration;

namespace Course.Infrastructure.Services;

public class AppSettigns : IAppSettings
{

    public string ApplicationName { get; init; }
    public AuthSettings AuthSettings { get; init; }
    public AppSettigns(IConfiguration configuration)
    {
        var applicationSection = configuration.GetSection("Application");

        ApplicationName = applicationSection["Name"] ?? "Unknown application name";
        AuthSettings = configuration.GetSection("Authentication").Get<AuthSettings>() ?? throw new InternalServerException("Не заданы настройки аутентификации");
    }
}