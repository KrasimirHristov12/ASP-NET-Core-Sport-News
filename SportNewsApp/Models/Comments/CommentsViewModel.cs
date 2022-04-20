using System.Collections.Generic;

namespace SportNewsApp.Models.Comments
{
    public class CommentsViewModel
    {
        public string ArticleId { get; set; }
        public ICollection<CommentsModel> Comments { get; set; }
    }
}
