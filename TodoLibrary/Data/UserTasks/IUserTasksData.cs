using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.UserTasks
{
    public interface IUserTasksData
    {
        void CreateUserTask(UserTasksModel task);
        void CompleteUserTask(int taskId);
        List<UserTasksModel> GetUserTasks(int userId);
    }
}