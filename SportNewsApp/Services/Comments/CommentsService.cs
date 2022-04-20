namespace SportNewsApp.Services.Comments
{
    using SportNewsApp.Data;
    using SportNewsApp.Data.Models;
    using SportNewsApp.Models.Comments;
    using SportNewsApp.Services.Users;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext data;
        private readonly IUsersService usersService;

        public CommentsService(ApplicationDbContext data, IUsersService usersService)
        {
            this.data = data;
            this.usersService = usersService;
        }
        public int CreateComment(string content, string userId, string articleId, int? mainCommentId = null)
        {
            var comment = new Comment()
            {
                Content = content,
                ArticleId = articleId,
                UserId = userId,
                MainCommentId = mainCommentId

            };
            this.data.Add(comment);
            this.data.SaveChanges();
            return comment.Id;
        }

        public ICollection<CommentsModel> GetAll(string articleId)
        {
            return this.data.Comments.Where(c => c.ArticleId == articleId)
                .Select(c => new CommentsModel
                {
                    Id = c.Id,
                    MainCommentId = c.MainCommentId,
                    Content = c.Content,
                    AuthorUsername = this.usersService.GetUsernameByUserId(c.UserId).GetAwaiter().GetResult(),
                    CreatedOn = c.CreatedOn

                    
                }).ToList();
        }
    }
}
