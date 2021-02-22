using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.UserTasks
{
    public interface IUserTasksData
    {
        /// <summary>
        /// Saves a new user task into database
        /// </summary>
        /// <param name="task">The task information</param>
        void CreateUserTask(UserTasksModel task);
        
        /// <summary>
        /// Marks task as finished in database.
        /// </summary>
        /// <param name="taskId">Unique identifier for task</param>
        void CompleteUserTask(int taskId);
        
        /// <summary>
        /// Get all tasks for given user from database
        /// </summary>
        /// <param name="userId">Unique identifier for user</param>
        /// <returns>List of all user tasks; empty list if no records found</returns>
        List<UserTasksModel> GetUserTasks(int userId);
    }
}