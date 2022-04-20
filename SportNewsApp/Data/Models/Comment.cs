using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportNewsApp.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            CreatedOn = DateTime.UtcNow;
        }
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ArticleId { get; set; }
        public Article Article { get; set; }
        public Comment MainComment { get; set; }
        public int? MainCommentId { get; set; }

    }
}
