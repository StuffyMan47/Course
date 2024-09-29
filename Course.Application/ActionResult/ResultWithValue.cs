using Course.Application.ActionResult.Enum;
using Course.Application.Exceptions;

namespace Course.Application.ActionResult;

public class Result<TValue>(ResultType type, TValue value) : Result(type)
{
    public readonly TValue Value = value;

    public static Result<TValue> Success(TValue value) => new(ResultType.Success, value);

    public new static Result<TValue> Invalid() => new(ResultType.Invalid, default!);

    public new static Result<TValue> Unauthorized() => new(ResultType.Unauthorized, default!);

    public new static Result<TValue> NotFound() => new(ResultType.NotFound, default!);

    public new static Result<TValue> PermissionDenied() => new(ResultType.PermissionDenied, default!);

    public override Result<TNew> As<TNew>() =>
        new Result<TNew>(Type, default!)
            .WithMessage(Message!)
            .WithErrors(Errors!);

    public override Result<TValue> WithMessage(string message)
    {
        Message = message;
        return this;
    }

    public override Result<TValue> WithError(string error)
    {
        if (Type == ResultType.Success)
            throw new InternalServerException("Cannot add error for success result");

        if (Errors is null)
            Errors = [error];
        else
            Errors.Add(error);

        return this;
    }

    public override Result<TValue> WithErrors(List<string> errors)
    {
        if (Type == ResultType.Success)
            throw new InternalServerException("Cannot add error for success result");

        Errors = errors;
        return this;
    }

    public override Result<TValue> WithCursor(long cursor)
    {
        Cursor = cursor;
        return this;
    }
}