namespace SportNewsApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Author
    {
        public Author()
        {
            Articles = new HashSet<Article>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(AuthorUsernameMaxLength)]
        public string Username { get; set; }
        public ICollection<Article> Articles { get; set; }
        public string UserId { get; set; }
    }
}