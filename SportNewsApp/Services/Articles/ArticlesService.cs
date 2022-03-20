namespace SportNewsApp.Services.Articles
{
    using SportNewsApp.Data;
    using SportNewsApp.Data.Models;
    using SportNewsApp.Models.Articles;
    using SportNewsApp.Services.Authors;
    using SportNewsApp.Services.Categories;
    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext data;
        private readonly ICategoriesService categoriesService;
        private readonly IAuthorsService authorsService;

        public ArticlesService(ApplicationDbContext data, ICategoriesService categoriesService, IAuthorsService authorsService)
        {
            this.data = data;
            this.categoriesService = categoriesService;
            this.authorsService = authorsService;
        }
        public string AddArticle(AddArticleInputModel article, int authorId)
        {
            var dataArticle = new Article 
            {
                Title = article.Title,
                CategoryId = this.categoriesService.GetCategoryIdByName(article.Category),
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                AuthorId = authorId,
                
                
            };
            this.data.Articles.Add(dataArticle);
            this.data.SaveChanges();
            return dataArticle.Id;

        }

    }
}
