namespace SportNewsApp.Services.Articles
{
    using SportNewsApp.Models.Articles;
    using System.Collections.Generic;

    public interface IArticlesService
    {
        string AddArticle(AddArticleInputModel article, int authorId);
        bool DeleteArticle(string id, string userId);

        int EditArticle(AddArticleInputModel article, string articleId,  string userId); // returns -1 if not found, returns -2 if unathorised, returns number of edited entries if successful
        ICollection<AllArticlesViewModel> GetAll();
        AddArticleInputModel GetArticle(string id);
        ArticleViewModel GetArticleViewModel(string id);
        ICollection<AllArticlesViewModel> GetYours(string userId);
        bool CheckIfArticleBelongsToAuthor(int currentAuthorId, int actualAuthorId);
        int GetAuthorId(string id);
       


    }
}
