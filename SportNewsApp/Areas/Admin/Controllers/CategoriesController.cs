namespace SportNewsApp.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Areas.Admin.Models.Categories;
    using SportNewsApp.Services.Categories;

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddCategoryInputModel category)
        {
            if (this.categoriesService.ExistsCategoryByName(category.Name))
            {
                ModelState.AddModelError("Name",$"The category with name '{category.Name}' already exists");
            }
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            this.categoriesService.CreateCategory(category.Name);
            return Redirect("/");

        }
    }
}
