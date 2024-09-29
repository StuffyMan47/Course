namespace Course.Application.Services.AppSettings.Models;

public class AuthSettings
{
    public required string ApiSecret { get; init; }
    public required string TokenLifetimeMinutes { get; init; }
    public required string RefreshTokenLifetimeMinutes { get; init; }
    public required int MaxSessionsPerUser { get; init; } = 5; // надо перенести в appsettings.json
}