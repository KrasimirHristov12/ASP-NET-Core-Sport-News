namespace SportNewsApp.Models.Articles
{
    using System.Collections.Generic;
    public class ArticlePageViewModel
    {
        public string Category { get; set; }
        public ICollection<AllArticlesViewModel> Articles { get; set; }
        public ICollection<string> AllCategories { get; set; }
        public int? CurrentPage { get; set; }
    }
}
