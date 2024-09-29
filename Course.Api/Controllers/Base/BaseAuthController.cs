using Course.Application.Models;
using Course.Infrastructure.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = Startup.AuthorizedGroupName)]
[Authorize]
[ProducesResponseType(typeof(ApiResponseModel), 401)]
[ProducesResponseType(typeof(ApiResponseModel), 403)]
public class BaseAuthController : BaseController;