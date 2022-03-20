namespace SportNewsApp.Services.Categories
{
    using SportNewsApp.Data;
    using SportNewsApp.Data.Models;
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        public ICollection<string> GetAll();
        public bool ExistsCategoryByName(string name);
        public int GetCategoryIdByName(string name);
    }
}
