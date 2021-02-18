using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoLibrary.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public int CategoryModelId { get; set; }
        
        [ForeignKey("CategoryModelId")]
        public CategoryModel CategoryModel { get; set; }
    }
}
