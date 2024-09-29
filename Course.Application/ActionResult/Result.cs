using Course.Application.ActionResult.Enum;
using Course.Application.Exceptions;

namespace Course.Application.ActionResult;

public class Result
{
    public ResultType Type { get; }
    public string? Message { get; protected set; }
    public List<string>? Errors { get; protected set; }
    public long? Cursor { get; protected set; }

    protected Result(ResultType type)
    {
        Type = type;
    }

    public static Result Success() => new(ResultType.Success);

    public static Result Invalid() => new(ResultType.Invalid);

    public static Result Unauthorized() => new(ResultType.Unauthorized);

    public static Result NotFound() => new(ResultType.NotFound);

    public static Result PermissionDenied() => new(ResultType.PermissionDenied);

    public bool IsSuccess => Type == ResultType.Success;
    public bool IsFailure => Type != ResultType.Success;
    public bool IsInvalid => Type == ResultType.Invalid;
    public bool IsUnauthorized => Type == ResultType.Unauthorized;
    public bool IsNotFound => Type == ResultType.NotFound;
    public bool IsPermissionDenied => Type == ResultType.PermissionDenied;
    public bool IsPaginated => Cursor.HasValue;

    public virtual Result<T> As<T>() => new Result<T>(Type, default!)
        .WithMessage(Message!)
        .WithErrors(Errors!);

    public virtual Result WithMessage(string message)
    {
        Message = message;
        return this;
    }

    public virtual Result WithError(string error)
    {
        if (Type == ResultType.Success)
            throw new InternalServerException("Cannot add error for success result");

        if (Errors is null)
            Errors = [error];
        else
            Errors.Add(error);
        return this;
    }

    public virtual Result WithErrors(List<string> errors)
    {
        if (Type == ResultType.Success)
            throw new InternalServerException("Cannot add error for success result");

        Errors = errors;
        return this;
    }

    public virtual Result WithCursor(long cursor)
    {
        Cursor = cursor;
        return this;
    }
}
