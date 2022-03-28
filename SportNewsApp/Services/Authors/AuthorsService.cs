namespace SportNewsApp.Services.Authors
{
    using SportNewsApp.Data;
    using SportNewsApp.Data.Models;
    using System.Linq;
    public class AuthorsService : IAuthorsService
    {
        private readonly ApplicationDbContext data;

        public AuthorsService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public string CreateAuthor(string userId, string username)
        {

            Author author = new Author
            {
                UserId = userId,
                Username = username
            };
            this.data.Authors.Add(author);
            this.data.SaveChanges();
            return author.UserId;
        }

       

        public bool IsUserAuthor(string userId)
        {
            return this.data.Authors.Any(a => a.UserId == userId);
        }


        public int GetAuthorId(string userId)
        {
            return data.Authors.Where(a => a.UserId == userId)
                .Select(a => a.Id)
                .FirstOrDefault();
        }
    }
}
