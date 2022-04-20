namespace SportNewsApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SportNewsApp.Services.Comments;
    using SportNewsApp.Services.Users;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;


        public CommentsController(ICommentsService commentsService, IUsersService usersService)
        {
            this.commentsService = commentsService;
            this.usersService = usersService;
        }
        [HttpPost]
        public IActionResult Add(string content, string id, int? mainCommentId)
        {
            var queryStringObj = new
            {
                Id = id
            };
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ArticlesController.Index), nameof(ArticlesController).Replace("Controller", ""), queryStringObj);
            }
            this.commentsService.CreateComment(content, this.usersService.GetUserId(User) , id, mainCommentId);

            return RedirectToAction(nameof(ArticlesController.Index), nameof(ArticlesController).Replace("Controller", ""), queryStringObj);
        }
    }
}
