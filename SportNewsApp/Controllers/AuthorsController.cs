namespace SportNewsApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Services.Authors;
    using SportNewsApp.Services.Users;

    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authorsService;
        private readonly IUsersService usersService;

        public AuthorsController(IAuthorsService authorsService, IUsersService usersService)
        {
            this.authorsService = authorsService;
            this.usersService = usersService;
        }
        [Authorize]
        public IActionResult Become()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [Route("Authors/Become")]
        public IActionResult BecomeAuthor()
        {
            string userId = this.usersService.GetUserId(User);
            string username = this.usersService.GetUsername(User);
            this.authorsService.CreateAuthor(userId,username);
            return RedirectToAction(nameof(ArticlesController.Add),nameof(ArticlesController).Replace("Controller", string.Empty));
        }


    }
}
