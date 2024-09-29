using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Course.Application.Constants;
using Course.Application.Services.AppSettings;
using Course.Application.Services.TokenService;
using Course.Application.Services.TokenService.Models;
using Course.Application.UseCases.Users.GenerateToken.Models;
using Course.Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;

namespace Course.Infrastructure.Authentication.JwtTokens;

public class JwtTokenService(IAppSettings settings) : IJwtTokenService
{

    public ValidateTokenResponse ValidateAccessToken(string accessToken, bool isValidateWithExpiry = true)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(settings.AuthSettings.ApiSecret);
        try
        {
            tokenHandler.ValidateToken(accessToken, new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = isValidateWithExpiry,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == TokenClaimKeys.UserId).Value);
            var userRole = Enum.Parse<UserRole>(jwtToken.Claims.First(x => x.Type == TokenClaimKeys.UserRole).Value);
            string login = jwtToken.Claims.First(x => x.Type == TokenClaimKeys.Login).Value;

            bool isGenerationSuccessful = userId != Guid.Empty &&
                                          userRole != UserRole.User &&
                                          login != "anonymous";

            return new()
            {
                IsSuccesss = isGenerationSuccessful,
                UserId = userId,
                UserRole = userRole,
                Login = login,
            };
        }
        catch
        {
            return new()
            {
                IsSuccesss = false,
                UserId = Guid.Empty,
                UserRole = UserRole.User,
                Login = "anonymous",
            };
        }
    }

    public string GenerateAcceesToken(GenerateTokenRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(settings.AuthSettings.ApiSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new(new[]
            {
                new Claim(TokenClaimKeys.UserId, request.UserId.ToString()),
                new Claim(TokenClaimKeys.UserRole, Enum.GetName(request.UserRole) ?? string.Empty),
                new Claim(TokenClaimKeys.Login, request.Login),
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(settings.AuthSettings.TokenLifetimeMinutes)),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    public string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[64];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}
