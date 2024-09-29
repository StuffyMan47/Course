using Course.Api.Controllers.Base;
using Course.Application.Models;
using Course.Application.UseCases.Users.Login.Models;
using Course.Application.UseCases.Users.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers.Anonymous;

public class ArticlesController : BaseController
{
    /// <summary>
    /// Получить список новостей для ленты
    /// </summary>
    /// <returns></returns>
    [HttpGet("articles/feed")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseApiResponseModel<LoginResponse>), 200)]
    public async Task<IActionResult> Feed([FromServices] ArticlesUseCase useCase)
    {
        var result = useCase.GetArticlesList();
        return FromResult(result);
    }

    /// <summary>
    /// Получить полную информацию о статье по id
    /// </summary>
    /// <returns></returns>
    [HttpGet("articles/details")]
    public async Task<IActionResult> ArticleDetails([FromServices] ArticlesUseCase useCase)
    {
        var result = useCase.GetArticlesList();
        return FromResult(result);
    }
}