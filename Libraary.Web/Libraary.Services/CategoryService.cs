﻿namespace Libraary.Services
{
    using Data;
    using Domain;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly LibraaryDbContext db;

        public CategoryService(LibraaryDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<string> GetCategories()
        {
            return this.db.Categories.Select(c => c.CategoryName).ToList();
        }

        public Category GetCategory(string category)
        {
            return this.db.Categories.FirstOrDefault(c => c.CategoryName == category);
        }
    }
}
