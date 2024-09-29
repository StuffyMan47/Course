using Course.Application.ActionResult;

namespace Course.Application.Errors;

public static partial class ErrorProvider
{
    public static class Users
    {
        public static Result NotFound(Guid userId) => Result.Invalid()
            .WithMessage("Пользователь не найден")
            .WithError($"UserId: {userId}");

        public static Result NoUserEmployee() => Result.Invalid()
            .WithMessage("Пользователю не назначен сотрудник");

        public static Result LoginAlreadyTaken(string login) => Result.Invalid()
            .WithMessage($"Пользователь с логином {login} уже существует");
    }
}