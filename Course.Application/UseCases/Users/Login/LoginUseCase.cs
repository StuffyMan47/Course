using Course.Application.ActionResult;
using Course.Application.Errors;
using Course.Application.Services;
using Course.Application.UseCases.Users.GenerateToken;
using Course.Application.UseCases.Users.Login.Interfaces;
using Course.Application.UseCases.Users.Login.Models;

namespace Course.Application.UseCases.Users.Login;

public class LoginUseCase(GenerateTokenUseCase tokenGenerator, ILoginStorage storage, IPasswordService passwordService)
{
    public async Task<Result<LoginResponse>> Login(LoginRequest request)
    {
        var user = await storage.GetUser(request.Login);

        if (user == null)
            return ErrorProvider.Auth.InvalidLoginOrPassword.As<LoginResponse>();

        bool isPasswordValid = passwordService.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt);
        if (!isPasswordValid)
            return ErrorProvider.Auth.InvalidLoginOrPassword.As<LoginResponse>();

        var token = await tokenGenerator.GenerateToken(new()
        {
            UserId = user.UserId,
            UserRole = user.UserRole,
            Login = user.Login,
        });

        return token.IsFailure
            ? token.As<LoginResponse>()
            : Result<LoginResponse>.Success(new(token.Value.AccessToken, token.Value.RefreshToken));
    }
}