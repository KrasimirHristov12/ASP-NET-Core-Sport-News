namespace SportNewsApp.Services.Categories
{
    using SportNewsApp.Data;
    using SportNewsApp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext data;

        public CategoriesService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public ICollection<string> GetAll()
        {
            return this.data.Categories
                .Select(c => c.Name)
                .ToList();
        }

        public bool ExistsCategoryByName(string name)
        {
            return this.data.Categories.Any(c => c.Name == name);
        }
        public int GetCategoryIdByName(string name)
        {
            var category = this.data.Categories.FirstOrDefault(c => c.Name == name);
            if (category == null)
            {
                return 0;
            }
            return category.Id;
        }

        public int CreateCategory(string name)
        {
            var category = new Category()
            {
                Name = name
            };
            this.data.Categories.Add(category);
            this.data.SaveChanges();
            return category.Id;
        }
    }
}
