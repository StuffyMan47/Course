using Course.Application.ActionResult;
using Course.Application.Errors;
using Course.Application.Services.TokenService;
using Course.Application.UseCases.Users.GenerateToken;
using Course.Application.UseCases.Users.RefreshToken.Interfaces;
using Course.Application.UseCases.Users.RefreshToken.Models;

namespace Course.Application.UseCases.Users.RefreshToken;

public class RefreshTokenUseCase(IRefreshTokenStorage storage, GenerateTokenUseCase tokenGenerator, IJwtTokenService tokenService)
{
    public async Task<Result<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var tokenValidation = tokenService.ValidateAccessToken(request.AccessToken, false);

        if (!tokenValidation.IsSuccesss)
            return ErrorProvider.Auth.InvalidAccessToken.As<RefreshTokenResponse>();

        var user = await storage.GetUser(tokenValidation.UserId, request.RefreshToken);
        if (user == null)
            return ErrorProvider.Users.NotFound(tokenValidation.UserId).As<RefreshTokenResponse>();

        if (user.RefreshTokenFromDb == null)
            return ErrorProvider.Auth.RefreshTokenWasNotIssued.As<RefreshTokenResponse>();

        bool isRefreshTokenExpired = user.RefreshTokenExpires == null || user.RefreshTokenExpires.Value.ToUniversalTime() < DateTimeOffset.UtcNow;
        if (isRefreshTokenExpired)
            return ErrorProvider.Auth.InvalidRefreshToken.As<RefreshTokenResponse>();

        int? ownerId = user.DefaultOwnerId;
        if (request.OwnerId != null)
        {
            if (!user.AvaliableEmployees.Select(e => e.OwnerId).Contains(request.OwnerId.Value))
                return Result.Unauthorized().WithMessage("Недостаточно прав").As<RefreshTokenResponse>();

            ownerId = request.OwnerId;
        }

        var token = await tokenGenerator.GenerateToken(new()
        {
            UserId = user.UserId,
            UserRole = user.UserRole,
            Login = user.Login,
        });

        return token.IsFailure
            ? token.As<RefreshTokenResponse>()
            : Result<RefreshTokenResponse>.Success(new(token.Value.AccessToken, token.Value.RefreshToken));
    }
}