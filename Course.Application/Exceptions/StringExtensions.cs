using System.Text.RegularExpressions;

namespace Course.Application.Exceptions;

public static partial class StringExtensions
{
    public static string ToLowerCaseWithUnderscore(this string value) => AnyUpperCasePrecededByNonUnderscoreChar().Replace(value, result => '_' + result.ToString().ToLower())
        .ToLower();

    public static string ToLowerCaseWithDash(this string value)
    {
        return AnyUpperCasePrecededByNonHyphenChar().Replace(value, result => '-' + result.ToString().ToLower())
            .ToLower();
    }

    public static string Truncate(this string? value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return value.Length <= maxLength ? value : value[..maxLength];
    }

    [GeneratedRegex("(?<=[^-])[A-Z]")]
    private static partial Regex AnyUpperCasePrecededByNonHyphenChar();

    [GeneratedRegex("(?<=[^_])[A-Z]")]
    private static partial Regex AnyUpperCasePrecededByNonUnderscoreChar();
    
}
