namespace SportNewsApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Category
    {
        public Category()
        {
            Articles = new HashSet<Article>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}