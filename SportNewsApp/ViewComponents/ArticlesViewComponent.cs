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
        public async Task<IViewComponentResult> InvokeAsync(string type, string teamName = null)
        {
            ICollection<AllArticlesViewModel> articles = null;
            if (type == "team")
            {
                articles = this.articlesService.GetArticlesForTeam(teamName);
            }
            return View(articles);
        }
    }
}
