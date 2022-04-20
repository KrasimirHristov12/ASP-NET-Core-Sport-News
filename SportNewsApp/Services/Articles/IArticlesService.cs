namespace SportNewsApp.Services.Articles
{
    using SportNewsApp.Models.Articles;
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface IArticlesService
    {
        string AddArticle(AddArticleInputModel article, int authorId);
        bool DeleteArticle(string id, ClaimsPrincipal user);

        int EditArticle(AddArticleInputModel article, string articleId, ClaimsPrincipal user); // returns -1 if not found, returns -2 if unathorised, returns number of edited entries if successful
        ICollection<AllArticlesViewModel> GetAll();
        ICollection<IndexArticlesViewModel> GetMostRecentArticles(int count);
        ICollection<AllArticlesViewModel> GetAllByCategory(string categoryName);
        AddArticleInputModel GetArticle(string id);
        ArticleViewModel GetArticleViewModel(string id);
        ICollection<AllArticlesViewModel> GetYours(string userId);
        bool CheckIfArticleBelongsToAuthor(int currentAuthorId, int actualAuthorId);
        int GetAuthorId(string id);
        ICollection<AllArticlesViewModel> GetArticlesForTeam(string teamName);
        int GetTodayArticlesCount();




    }
}
