using Course.Application.ActionResult;

namespace Course.Application.Errors;

public static partial class ErrorProvider
{
    public static List<string> GenerateErrorStringsFromResult(Result result)
    {
        var errors = new List<string>();

        if (result.IsSuccess || result.Message == null)
            return errors;

        if (result.Errors is { Count: > 0 })
        {
            result.Errors.ForEach(e => errors.Add($"{result.Message}: {e}"));
        }
        else
        {
            errors.Add(result.Message);
        }

        return errors;
    }
}
