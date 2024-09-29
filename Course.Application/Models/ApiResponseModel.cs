namespace Course.Application.Models;

public class ApiResponseModel
{
    public ApiResponseErrors? Errors { get; init; }
}

public class BaseApiResponseModel<T> : ApiResponseModel
{
    public T? Data { get; init; }
}

public class PaginatedApiResponseModel<T> : BaseApiResponseModel<T>
{
    public long Cursor { get; init; }
}