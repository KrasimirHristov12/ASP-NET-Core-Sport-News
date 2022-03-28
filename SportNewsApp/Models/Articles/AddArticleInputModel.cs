namespace SportNewsApp.Models.Articles
{
    using SportNewsApp.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static SportNewsApp.Data.DataConstants;
    public class AddArticleInputModel
    {
               
        [Display(Name = "Name")]
        [Required]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength)]
        public string Title { get; set; }

        [Display(Name = "Image Link")]
        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(ArticleContentMaxLength, MinimumLength = ArticleContentMinLength)]
        public string Content { get; set; }

        public ICollection<string> Categories { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength,MinimumLength = CategoryNameMinLength)]
        public string Category { get; set; }
    }
}
