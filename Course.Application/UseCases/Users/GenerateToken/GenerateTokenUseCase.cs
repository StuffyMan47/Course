using Course.Application.ActionResult;
using Course.Application.Services.AppSettings;
using Course.Application.Services.TokenService;
using Course.Application.UseCases.Users.GenerateToken.Interfaces;
using Course.Application.UseCases.Users.GenerateToken.Models;

namespace Course.Application.UseCases.Users.GenerateToken;

public class GenerateTokenUseCase(IJwtTokenService service, IAppSettings settings, IGenerateTokenStorage storage)
{
    public async Task<Result<GenerateTokenResponse>> GenerateToken(GenerateTokenRequest request)
    {
        string accessToken = service.GenerateAcceesToken(request);
        string refreshToken = service.GenerateRefreshToken();
        int expirationInMinutes = int.Parse(settings.AuthSettings.RefreshTokenLifetimeMinutes);

        var result = new GenerateTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshExpirationDate = DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes)
        };

        await storage.SaveUserRefreshToken(new()
        {
            RefreshToken = result.RefreshToken,
            TokenExpirationDate = result.RefreshExpirationDate,
            UserId = request.UserId
        });

        return Result<GenerateTokenResponse>.Success(result);
    }
}
