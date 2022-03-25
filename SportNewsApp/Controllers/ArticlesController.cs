﻿namespace SportNewsApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Models.Articles;
    using SportNewsApp.Services.Articles;
    using SportNewsApp.Services.Authors;
    using SportNewsApp.Services.Categories;
    using SportNewsApp.Services.Users;
    using System.Linq;
    using System.Security.Claims;

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
        [Authorize]
        public IActionResult Add()
        {
            var addModel = new AddArticleInputModel
            {
                Categories = categoriesService.GetAll()
            };
            return View(addModel);
        }

        [Authorize]
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
            articlesService.AddArticle(article, authorId);
            return Redirect("/");

            


        }

    }

}