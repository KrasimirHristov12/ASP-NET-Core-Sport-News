using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportNewsApp.Models;
using SportNewsApp.Services.Articles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SportNewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticlesService articles;

        public HomeController(ILogger<HomeController> logger, IArticlesService articles)
        {
            _logger = logger;
            this.articles = articles;
        }

        public IActionResult Index()
        {
            var latestArticles = this.articles.GetMostRecentArticles(5);
            return View(latestArticles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
