namespace SportNewsApp.Services.Articles
{
    using Microsoft.EntityFrameworkCore;
    using SportNewsApp.Data;
    using SportNewsApp.Data.Models;
    using SportNewsApp.Models.Articles;
    using SportNewsApp.Services.Authors;
    using SportNewsApp.Services.Categories;
    using SportNewsApp.Services.Users;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

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
        public ArticleViewModel GetArticleViewModel(string id)
        {
            return data.Articles.Where(a => a.Id == id)
                .Select(a => new ArticleViewModel
                {
                    Title = a.Title,
                    Content = a.Content,
                    CategoryName = a.Category.Name,
                    ImageUrl = a.ImageUrl,
                    CreatedOn = a.CreatedOn.ToString("dddd, dd MMMM yyyy"),
                    AuthorName = a.Author.Username
                })
                .FirstOrDefault();
        }
        public AddArticleInputModel GetArticle(string id)
        {
            return data.Articles.Where(a => a.Id == id)
                .Select(a => new AddArticleInputModel
                {
                    Title = a.Title,
                    Content = a.Content,
                    Category = a.Category.Name,
                    ImageUrl = a.ImageUrl,
                    Categories = this.categoriesService.GetAll()
                })
                .FirstOrDefault();
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
            return data.Articles
                .Select(a => new AllArticlesViewModel
                {
                        Id = a.Id,
                        ImageUrl = a.ImageUrl,
                        Title = a.Title
                })
                .ToList();
        }

        public ICollection<AllArticlesViewModel> GetAllByCategory(string categoryName)
        {
            return data.Articles.Where(a => a.Category.Name.ToLower() == categoryName)
                .Select(a => new AllArticlesViewModel
                 {
                     Id = a.Id,
                     ImageUrl = a.ImageUrl,
                     Title = a.Title
                 })
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
        public bool DeleteArticle(string id,ClaimsPrincipal user)
        {
            var article = data.Articles.FirstOrDefault(a => a.Id == id);
            var authorId = authorsService.GetAuthorId(this.usersService.GetUserId(user));
            
            if (article == null)
            {
                return false;
            }
            if (article.AuthorId != authorId && !this.usersService.IfUserAdmin(user))
            {
                return false;
            }
            data.Articles.Remove(article);
            data.SaveChanges();
            return true;
            


        }

        public int EditArticle(AddArticleInputModel article, string articleId, ClaimsPrincipal user)
        {
            var articleData = this.data.Articles.Include(a => a.Category).FirstOrDefault(a => a.Id == articleId);
            int countEditedEntries = 0;
            if (articleData == null)
            {
                return -1;
            }
            int currentAuthorId = this.authorsService.GetAuthorId(this.usersService.GetUserId(user));
            if (!CheckIfArticleBelongsToAuthor(currentAuthorId,articleData.AuthorId) && !this.usersService.IfUserAdmin(user))
            {
                return -2;
            }
            if (articleData.Title != article.Title)
            {
                articleData.Title = article.Title;
                countEditedEntries++;
            }
            if (articleData.Content != article.Content)
            {
                articleData.Content = article.Content;
                countEditedEntries++;
            }
            if (articleData.ImageUrl != article.ImageUrl)
            {
                articleData.ImageUrl = article.ImageUrl;
                countEditedEntries++;
            }
            if (articleData.Category.Name != article.Category)
            {
                if (!this.categoriesService.ExistsCategoryByName(article.Category))
                {
                    return -1;
                }
               int categoryId = this.categoriesService.GetCategoryIdByName(article.Category);
               articleData.CategoryId = categoryId;
               countEditedEntries++;

            }
            
            this.data.SaveChanges();
            return countEditedEntries;
            
        }

        public bool CheckIfArticleBelongsToAuthor(int currentAuthorId, int actualAuthorId)
        {
            return currentAuthorId == actualAuthorId;
        }
        public int GetAuthorId(string id)
        {
            return data.Articles.Where(a => a.Id == id)
                .Select(a => a.AuthorId)
                .FirstOrDefault();
             
        }

        public ICollection<IndexArticlesViewModel> GetMostRecentArticles(int count)
        {
            return this.data.Articles.Take(count).Select(a => new IndexArticlesViewModel
            {
                Id = a.Id,
                Title = a.Title,
                ImageUrl = a.ImageUrl
            })
                .ToList();
        }

        public ICollection<AllArticlesViewModel> GetArticlesForTeam(string teamName)
        {
            var articles = this.data.Articles.Where(a => a.Title.ToLower().Contains(teamName.ToLower()) || a.Content.ToLower().Contains(teamName.ToLower()))
                .Select(a => new AllArticlesViewModel
                {
                    Id = a.Id,
                    ImageUrl = a.ImageUrl,
                    Title = a.Title
                })
                .ToList();
            return articles;
        }
    }
}
