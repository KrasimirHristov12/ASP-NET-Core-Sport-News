﻿namespace SportNewsApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Article
    {
        public Article()
        {
            Id = Guid.NewGuid().ToString();
            CreatedOn = DateTime.UtcNow;
        }
        public string Id { get; set; }
        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(ArticleContentMaxLength)]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}