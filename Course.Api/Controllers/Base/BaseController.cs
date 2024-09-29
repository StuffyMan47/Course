using Course.Application.ActionResult;
using Course.Application.ActionResult.Enum;
using Course.Application.ActionResult.Extensions;
using Course.Application.Exceptions;
using Course.Infrastructure.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = Startup.AnonymousGroupName)]
public class BaseController : ControllerBase
{
    protected IActionResult FromResult(Result result)
{
    return result.Type switch
    {
        ResultType.Success => Ok(),
        ResultType.Unauthorized => Unauthorized(result.PackAsApiResponse()),
        ResultType.PermissionDenied => StatusCode(StatusCodes.Status403Forbidden, result.PackAsApiResponse()),
        ResultType.NotFound => BadRequest(result.PackAsApiResponse()),
        ResultType.Invalid => BadRequest(result.PackAsApiResponse()),
        _ => throw new InternalServerException(
            $"{Enum.GetName(typeof(ResultType), result.Type)} result type not implemented for transform to api response"
        )
    };
}

protected IActionResult FromResult<T>(Result<T> result)
{
    if (result.IsPaginated)
    {
        return result.Type switch
        {
            ResultType.Success => Ok(result.PackAsPaginatedApiResponse()),
            ResultType.Unauthorized => Unauthorized(result.PackAsPaginatedApiResponse()),
            ResultType.PermissionDenied => StatusCode(StatusCodes.Status403Forbidden, result.PackAsPaginatedApiResponse()),
            ResultType.NotFound => BadRequest(result.PackAsPaginatedApiResponse()),
            ResultType.Invalid => BadRequest(result.PackAsPaginatedApiResponse()),
            _ => throw new InternalServerException(
                $"{Enum.GetName(typeof(ResultType), result.Type)} result type not implemented for transform to api response"
            )
        };
    }
    return result.Type switch
    {
        ResultType.Success => Ok(result.PackAsApiResponse()),
        ResultType.Unauthorized => Unauthorized(result.PackAsApiResponse()),
        ResultType.PermissionDenied => StatusCode(StatusCodes.Status403Forbidden, result.PackAsApiResponse()),
        ResultType.NotFound => BadRequest(result.PackAsApiResponse()),
        ResultType.Invalid => BadRequest(result.PackAsApiResponse()),
        _ => throw new InternalServerException(
            $"{Enum.GetName(typeof(ResultType), result.Type)} result type not implemented for transform to api response"
        )
    };
}
}