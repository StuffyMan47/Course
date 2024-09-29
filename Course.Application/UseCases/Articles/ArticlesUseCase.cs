using Course.Application.ActionResult;

namespace Course.Application.UseCases.Users.News;

public class ArticlesUseCase
{
    public Result<String> GetArticlesList()
    {
        Console.WriteLine("kek");
        return Result<string>.Success("news feed");
    }
}