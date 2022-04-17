using SportNewsApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportNewsApp.Services.Authors
{
    public interface IAuthorsService
    {
        string CreateAuthor(string userId, string username);
        bool IsUserAuthor(string userId);

        int GetAuthorId(string userId);
        bool DeleteAuthor(string userId);


    }
}
