using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoLibrary.Models
{
    /// <summary>
    /// Represent the task for particular user.
    /// </summary>
    public class UserTasksModel
    {
        /// <summary>
        /// The unique identifier for User Task.
        /// </summary>
        public int Id { get; set; } 
        
        /// <summary>
        /// The numeric identifier of priority for the task. 
        /// </summary>
        [Required]
        [Range(1, 10)]
        public int Priority { get; set; }

        /// <summary>
        /// Date when the task has been created.
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DateStarted { get; set; } = DateTime.Now;

        /// <summary>
        /// Date when the task is supposed to be completed.
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DateDeadLine { get; set; }

        /// <summary>
        /// Represents if the given task is completed or not.
        /// </summary>
        [Required]
        public bool IsFinished { get; set; } = false;

        /// <summary>
        /// The unique identifier of User.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property for the User.
        /// </summary>
        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        
        /// <summary>
        /// The unique identifier of task.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Navigation property for the Task.
        /// </summary>
        [ForeignKey("TaskId")]
        public TaskModel Task { get; set; }
    }
}