namespace SportNewsApp.Areas.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using SportNewsApp.Data;

    public class AddCategoryInputModel
    {
        [StringLength(DataConstants.CategoryNameMaxLength,MinimumLength = DataConstants.CategoryNameMinLength)]
        [Required]
        public string Name { get; set; }
    }
}
