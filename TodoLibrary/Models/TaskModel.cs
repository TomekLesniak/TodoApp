using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoLibrary.Models
{
    /// <summary>
    /// Represents one task.
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// The unique identifier for Task
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The title of task.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        /// <summary>
        /// Description for the task.
        /// </summary>
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// The unique identifier of assigned category.
        /// </summary>
        public int CategoryModelId { get; set; }
        
        /// <summary>
        /// Navigation property for CategoryModel
        /// </summary>
        [ForeignKey("CategoryModelId")]
        public CategoryModel CategoryModel { get; set; }
    }
}
