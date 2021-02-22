using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Categories
{
    public interface ICategoriesData
    {
        /// <summary>
        /// Saves a new category into database.
        /// </summary>
        /// <param name="category">The category information</param>
        void CreateCategory(CategoryModel category);

        /// <summary>
        /// Get category by id from database.
        /// </summary>
        /// <param name="id">Unique identifier for category.</param>
        /// <returns>Found category; null otherwise</returns>
        CategoryModel GetCategory(int id);

        /// <summary>
        /// Get all categories from database.
        /// </summary>
        /// <returns>List of all available categories; empty list if no records found</returns>
        List<CategoryModel> GetCategories();
    }
}