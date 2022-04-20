namespace SportNewsApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Models.Articles;
    using SportNewsApp.Services.Articles;
    using SportNewsApp.Services.Authors;
    using SportNewsApp.Services.Categories;
    using SportNewsApp.Services.Users;
    using System.Collections.Generic;
    using System.Linq;

    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;
        private readonly ICategoriesService categoriesService;
        private readonly IAuthorsService authorsService;
        private readonly IUsersService usersService;

        public ArticlesController(IArticlesService articlesService,ICategoriesService categoriesService,IAuthorsService authorsService
            ,IUsersService usersService)
        {
            this.articlesService = articlesService;
            this.categoriesService = categoriesService;
            this.authorsService = authorsService;
            this.usersService = usersService;
        }

        public IActionResult Index(string id)
        {
            var article = this.articlesService.GetArticleViewModel(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
        public IActionResult All(string category, int? page)
        {
            ICollection<AllArticlesViewModel> allArticles = null;
           
            if (category == null)
            {
                allArticles = articlesService.GetAll();
            }
            else
            {
                allArticles = articlesService.GetAllByCategory(category);
            }
            var articles = new ArticlePageViewModel()
            {
                Category = category != null ? category.ToLower() : null,
                AllCategories = this.categoriesService.GetAll().Select(c => c.ToLower()).ToList(),
                Articles = allArticles,
                CurrentPage = page
            };
            return View(articles);

            //Implement search articles which contains the team name
        }

        [Authorize(Roles = "Author")]
        public IActionResult Add()
        {
            var addModel = new AddArticleInputModel
            {
                Categories = categoriesService.GetAll()
            };
            return View(addModel);
        }

        [Authorize(Roles = "Author")]
        [HttpPost]
        public IActionResult Add(AddArticleInputModel article)
        {
            if (!categoriesService.ExistsCategoryByName(article.Category))
            {
                ModelState.AddModelError("Category", $"The specified category '{article.Category}' does not exist");

            }
            if (!ModelState.IsValid)
            {
                article.Categories = categoriesService.GetAll();
                return View(article);
            }
            var authorId = this.authorsService.GetAuthorId(this.usersService.GetUserId(User));
            if (authorId == 0)
            {
                return BadRequest();
            }
            this.articlesService.AddArticle(article, authorId);
            return RedirectToAction(nameof(Yours));
            
       }
        [Authorize(Roles = "Author")]
        public IActionResult Yours()
        {
            var articles = this.articlesService.GetYours(this.usersService.GetUserId(User));
            return View(articles);
        }
        [Authorize(Roles = "Author")]
        public IActionResult Delete(string id)
        {
            bool result = articlesService.DeleteArticle(id, User);
            if (!result && !this.usersService.IfUserAdmin(User))
            {
                return Unauthorized();
            }
            return RedirectToAction(!this.usersService.IfUserAdmin(User) ? nameof(Yours) : nameof(All));
        }

        [Authorize(Roles = "Author")]
        public IActionResult Edit(string id)
        {
            var myArticle = articlesService.GetArticle(id);
            var currentAuthorId = this.authorsService.GetAuthorId(this.usersService.GetUserId(User));
            var articleAuthorId = this.articlesService.GetAuthorId(id);
            if (myArticle == null)
            {
                return NotFound();
            }
            if (!this.articlesService.CheckIfArticleBelongsToAuthor(currentAuthorId,articleAuthorId) && !usersService.IfUserAdmin(User))
            {
                return Unauthorized();
            }
            return View(myArticle);
        }
        [Authorize(Roles = "Author,Admin")]
        [HttpPost]
        public IActionResult Edit(string id,AddArticleInputModel article)
        {
            string userId = this.usersService.GetUserId(User);
            int authorId = this.authorsService.GetAuthorId(userId);
            int result = this.articlesService.EditArticle(article, id, User);
            if (User.IsInRole("Author"))
            {
                if (result == -1)
                {
                    return NotFound();
                }
                else if (result == -2)
                {
                    return Unauthorized();
                }
            }


            return RedirectToAction(!this.usersService.IfUserAdmin(User) ? nameof(Yours) : nameof(All));
        }


    }

}
