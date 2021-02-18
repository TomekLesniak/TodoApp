using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.UserTasks
{
    public interface IUserTasksData
    {
        void CreateUserTask(UserTasksModel task);
        List<UserTasksModel> GetUserTasks(int userId);
    }
}