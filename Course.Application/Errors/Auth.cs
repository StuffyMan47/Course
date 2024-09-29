using Course.Application.ActionResult;

namespace Course.Application.Errors;

public static partial class ErrorProvider
{
    public static class Auth
    {
        public static Result InvalidLoginOrPassword => Result.Invalid().WithMessage("Неверный логин или пароль");
        public static Result InvalidAccessToken => Result.Invalid().WithMessage("Невалидный access token");
        public static Result InvalidRefreshToken => Result.Invalid().WithMessage("Невалидный refresh token");
        public static Result RefreshTokenWasNotIssued => Result.Invalid().WithMessage("Refresh token не выдавался");
        public static Result OnlyForAuthorized => Result.Unauthorized().WithMessage("Доступ только авторизованным пользователям");
    }
}