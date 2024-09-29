using Course.Application.Services.TokenService.Models;
using Course.Application.UseCases.Users.GenerateToken.Models;

namespace Course.Application.Services.TokenService;

public interface IJwtTokenService
{
    ValidateTokenResponse ValidateAccessToken(string accessToken, bool isValidateWithExpiry = true);
    string GenerateAcceesToken(GenerateTokenRequest request);
    string GenerateRefreshToken();
}