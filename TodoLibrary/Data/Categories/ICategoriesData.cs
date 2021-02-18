using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Categories
{
    public interface ICategoriesData
    {
        void CreateCategory(CategoryModel category);
        List<CategoryModel> GetCategories();
    }
}