using Microsoft.AspNetCore.Mvc;
using SportNewsApp.Models.Comments;
using SportNewsApp.Services.Comments;
using System.Threading.Tasks;

namespace SportNewsApp.ViewComponents
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly ICommentsService commentsService;

        public CommentsViewComponent(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string articleId)
        {
            var comments = new CommentsViewModel()
            {
                ArticleId = articleId,
                Comments = this.commentsService.GetAll(articleId)
            };
                
            return View(comments);
        }
    }
}
