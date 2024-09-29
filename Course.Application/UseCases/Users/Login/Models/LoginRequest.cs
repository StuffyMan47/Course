using FluentValidation;

namespace Course.Application.UseCases.Users.Login.Models;

public record LoginRequest(string Login, string Password);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty().WithName("Имя пользователя");
        RuleFor(x => x.Password).NotEmpty().WithName("Пароль");
    }
}