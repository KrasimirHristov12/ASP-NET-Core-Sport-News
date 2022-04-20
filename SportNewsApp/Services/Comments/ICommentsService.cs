using SportNewsApp.Models.Comments;
using System.Collections.Generic;
using System.Security.Claims;

namespace SportNewsApp.Services.Comments
{
    public interface ICommentsService
    {
        int CreateComment(string content, string userId, string articleId, int? mainCommentId = null);
        ICollection<CommentsModel> GetAll(string articleId);
    }
}
