namespace SportNewsApp.Services.Articles
{
    using SportNewsApp.Data;
    using SportNewsApp.Data.Models;
    using SportNewsApp.Models.Articles;
    using SportNewsApp.Services.Authors;
    using SportNewsApp.Services.Categories;
    using SportNewsApp.Services.Users;
    using System.Collections.Generic;
    using System.Linq;

    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext data;
        private readonly ICategoriesService categoriesService;
        private readonly IAuthorsService authorsService;
        private readonly IUsersService usersService;

        public ArticlesService(ApplicationDbContext data, ICategoriesService categoriesService, IAuthorsService authorsService,
            IUsersService usersService)
        {
            this.data = data;
            this.categoriesService = categoriesService;
            this.authorsService = authorsService;
            this.usersService = usersService;
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
        

 

        public ICollection<AllArticlesViewModel> GetAll()
        {
            return data.Articles.Select
                (
                    a => new AllArticlesViewModel
                    {
                        Id = a.Id,
                        ImageUrl = a.ImageUrl,
                        Title = a.Title
                    }
                )
                .ToList();
        }

        public ICollection<AllArticlesViewModel> GetYours(string userId)
        {
            int authorId = authorsService.GetAuthorId(userId);
            return data.Articles
                .Where(a => a.AuthorId == authorId)
                .Select
                (
                    a => new AllArticlesViewModel
                    {
                        Id = a.Id,
                        ImageUrl = a.ImageUrl,
                        Title = a.Title
                    }
                )
                   .ToList();
        }
        public bool DeleteArticle(string id,string userId)
        {
            var article = data.Articles.FirstOrDefault(a => a.Id == id);
            var authorId = authorsService.GetAuthorId(userId);
            
            if (article == null)
            {
                return false;
            }
            if (article.AuthorId != authorId)
            {
                return false;
            }
            data.Articles.Remove(article);
            data.SaveChanges();
            return true;
            


        }
    }
}
