using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Tasks
{
    public interface ITasksData
    {
        /// <summary>
        /// Saves a new task into database
        /// </summary>
        /// <param name="task">The task information</param>
        void CreateTask(TaskModel task);
        
        /// <summary>
        /// Get task by id from database
        /// </summary>
        /// <param name="id">Unique identifier for task</param>
        /// <returns>Found task; null otherwise</returns>
        TaskModel GetTask(int id);
        
        /// <summary>
        /// Get all tasks from database
        /// </summary>
        /// <returns>List of all tasks; empty list if no records found</returns>
        List<TaskModel> GetTasks();
    }
}