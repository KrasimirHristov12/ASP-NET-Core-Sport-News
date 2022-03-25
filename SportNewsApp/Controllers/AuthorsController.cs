namespace SportNewsApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Services.Authors;
    using SportNewsApp.Services.Users;
    using System.Threading.Tasks;

    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authorsService;
        private readonly IUsersService usersService;
        private readonly UserManager<IdentityUser> userManager;

        public AuthorsController(IAuthorsService authorsService, IUsersService usersService, UserManager<IdentityUser> userManager)
        {
            this.authorsService = authorsService;
            this.usersService = usersService;
            this.userManager = userManager;
        }
        [Authorize(Roles = "User")]
        public IActionResult Become()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("Authors/Become")]
        public IActionResult BecomeAuthor()
        {
            string userId = this.usersService.GetUserId(User);
            string username = this.usersService.GetUsername(User);
            this.authorsService.CreateAuthor(userId,username);
            string role = "Author";
            Task.Run(async () =>
            {
                var user = await userManager.GetUserAsync(User);
                await userManager.RemoveFromRoleAsync(user, "User");
                var result =  await userManager.AddToRoleAsync(user, role);
            }).GetAwaiter()
            .GetResult();

            return RedirectToAction(nameof(ArticlesController.Add),nameof(ArticlesController).Replace("Controller", string.Empty));
        }


    }
}
