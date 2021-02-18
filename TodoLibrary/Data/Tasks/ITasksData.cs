using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Tasks
{
    public interface ITasksData
    {
        void CreateTask(TaskModel task);
        List<TaskModel> GetTasks();
    }
}