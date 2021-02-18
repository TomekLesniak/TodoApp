using System.ComponentModel.DataAnnotations;

namespace TodoLibrary.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
    }
}