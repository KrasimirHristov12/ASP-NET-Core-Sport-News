namespace SportNewsApp.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Models.Articles;
    using SportNewsApp.Services.Articles;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public ArticlesViewComponent(IArticlesService articles)
        {
            this.articlesService = articles;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int todaysCount = this.articlesService.GetTodayArticlesCount();
            return View(todaysCount);
        }
    }
}
