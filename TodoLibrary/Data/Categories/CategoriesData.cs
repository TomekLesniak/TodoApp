using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Categories
{
    public class CategoriesData : ICategoriesData
    {
        private readonly ApplicationDbContext _db;

        public CategoriesData(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void CreateCategory(CategoryModel category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public List<CategoryModel> GetCategories()
        {
            return _db.Categories.ToList();
        }
    }
}
