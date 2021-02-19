using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TodoLibrary.Models
{
    public class UserTasksModel
    {
        public int Id { get; set; } 
        
        [Required]
        [Range(1, 10)]
        public int Priority { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DateStarted { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DateDeadLine { get; set; }

        [Required]
        public bool IsFinished { get; set; } = false;

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public TaskModel Task { get; set; }
    }
}


/*
 * <StackPanel Orientation="Vertical">
                                <TextBlock Text="{Binding Path=Task.Title}"></TextBlock>
                                <TextBlock Text="{Binding Path=Task.Description}"></TextBlock>
                            </StackPanel>*/