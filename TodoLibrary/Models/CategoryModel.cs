using System.ComponentModel.DataAnnotations;

namespace TodoLibrary.Models
{
    /// <summary>
    /// Represents one category.
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// The unique identifier for Category
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The title of the category
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
    }
}