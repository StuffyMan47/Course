using Course.Api.Controllers.Base;
using Course.Application.Models;
using Course.Application.UseCases.Users.Login.Models;
using Course.Application.UseCases.Users.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers.Anonymous;

public class ArticlesAuthController : BaseAuthController
{
    /// <summary>
    /// Создать статью
    /// </summary>
    /// <returns></returns>
    [HttpPost("articles/create")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseApiResponseModel<LoginResponse>), 200)]
    public async Task<IActionResult> Login([FromServices] ArticlesUseCase useCase, [FromBody] LoginRequest request)
    {
        var result = useCase.GetArticlesList();
        return FromResult(result);
    }
}