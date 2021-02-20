using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Categories
{
    public interface ICategoriesData
    {
        void CreateCategory(CategoryModel category);
        CategoryModel GetCategory(int id);
        List<CategoryModel> GetCategories();
    }
}