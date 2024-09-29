using Course.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Course.Api.Filters;

public class ModelValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        var errors = context.ModelState
            .SelectMany(state => state.Value!.Errors
                .Select(error => new
                {
                    fieldName = state.Key,
                    message = error.ErrorMessage
                })
            )
            .Select(error => error.message)
            .ToList();

        var result = new ApiResponseModel
        {
            Errors = new()
            {
                Message = "Данные не прошли валидацию",
                Descriptions = errors
            }
        };
        context.Result = new JsonResult(result);
        context.HttpContext.Response.StatusCode = 400;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}