namespace SportNewsApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Article
    {
        public Article()
        {
            Id = Guid.NewGuid().ToString();
            CreatedOn = DateTime.UtcNow;
            Comments = new HashSet<Comment>();
        }
        public string Id { get; set; }
        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(ArticleContentMaxLength)]
        public string Content { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }


    }
}
