namespace SportNewsApp.Services.Articles
{
    using SportNewsApp.Models.Articles;

    public interface IArticlesService
    {
        string AddArticle(AddArticleInputModel article, int authorId);
    }
}
