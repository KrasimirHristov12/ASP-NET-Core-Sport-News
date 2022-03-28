namespace SportNewsApp.Services.Articles
{
    using SportNewsApp.Models.Articles;
    using System.Collections.Generic;

    public interface IArticlesService
    {
        string AddArticle(AddArticleInputModel article, int authorId);
        ICollection<AllArticlesViewModel> GetAll();
        ICollection<AllArticlesViewModel> GetYours(string userId);
        bool DeleteArticle(string id, string userId);

    }
}
