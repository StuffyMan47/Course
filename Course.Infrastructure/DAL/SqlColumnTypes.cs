namespace Course.Infrastructure.DAL;

public static class SqlColumnTypes
{
    public const string TimeStamp = "timestamp";
    public const string TimeStampWithTimezone = "timestamp with time zone";
    public const string JsonB = "jsonb";
    public const string Text = "text";

    public static string Varchar(int length) => $"varchar({length})";

    public static string Decimal(int precision, int scale) => $"numeric({precision}, {scale})";
}