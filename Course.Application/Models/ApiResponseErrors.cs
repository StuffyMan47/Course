namespace Course.Application.Models;

public class ApiResponseErrors
{
    public string Message { get; set; } = string.Empty;
    public List<string> Descriptions { get; set; } = [];
}