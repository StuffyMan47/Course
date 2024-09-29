using Course.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Course.Api.Controllers.Utils;

public class ControllerNameConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.ControllerName = controller.ControllerName.ToLowerCaseWithDash();
    }
}