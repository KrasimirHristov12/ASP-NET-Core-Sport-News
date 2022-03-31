namespace SportNewsApp.Services.Categories
{
    using SportNewsApp.Data;
    using SportNewsApp.Data.Models;
    using System.Collections.Generic;

    public interface ICategoriesService
    {
         ICollection<string> GetAll();
         bool ExistsCategoryByName(string name);
         int GetCategoryIdByName(string name);

        int CreateCategory(string name);
    }
}
