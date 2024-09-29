using FluentValidation;

namespace Course.Application.UseCases.Users.RefreshToken.Models;

public record RefreshTokenRequest(string AccessToken, string RefreshToken, int? OwnerId = null);

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty().WithName("Токен доступа");
        RuleFor(x => x.RefreshToken).NotEmpty().WithName("Токен обновления");
    }
}