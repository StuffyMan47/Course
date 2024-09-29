using Course.Application.Services.AppSettings.Models;

namespace Course.Application.Services.AppSettings;

public interface IAppSettings
{
    string ApplicationName { get; init; }
    AuthSettings AuthSettings { get; init; }
}