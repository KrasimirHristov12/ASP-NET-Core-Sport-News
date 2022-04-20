namespace SportNewsApp.Models.Comments
{
    using System;
    public class CommentsModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? MainCommentId { get; set; }
        public string ArticleId { get; set; }
        public string AuthorUsername { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
