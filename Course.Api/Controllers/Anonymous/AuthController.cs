using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Course.Api.Controllers.Base;
using Course.Application.Models;
using Course.Application.UseCases.Users.Login;
using Course.Application.UseCases.Users.Login.Models;
using Course.Application.UseCases.Users.RefreshToken;
using Course.Application.UseCases.Users.RefreshToken.Models;

namespace Course.Api.Controllers.Anonymous;

public class AuthController : BaseController
{

    /// <summary>
    /// Получить токены для доступа к апи
    /// </summary>
    /// <response code="200">Аутентификация прошла успешно</response>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseApiResponseModel<LoginResponse>), 200)]
    public async Task<IActionResult> Login([FromServices] LoginUseCase useCase, [FromBody] LoginRequest request)
    {
        var result = await useCase.Login(request);
        return FromResult(result);
    }

    /// <summary>
    /// Обновить имеющийся токен
    /// </summary>
    /// <returns></returns>
    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseApiResponseModel<RefreshTokenResponse>), 200)]
    public async Task<IActionResult> Refresh([FromServices] RefreshTokenUseCase useCase, [FromBody] RefreshTokenRequest request)
    {
        var result = await useCase.RefreshToken(request);
        return FromResult(result);
    }

    /// <summary>
    /// Зарегестрироваться
    /// </summary>
    /// <returns></returns>
    [HttpPost("registration")]
    [AllowAnonymous]
    public async Task<IActionResult> Registration([FromServices] LoginUseCase useCase, [FromBody] LoginRequest request)
    {
        var result = await useCase.Login(request);
        return FromResult(result);
    }
}