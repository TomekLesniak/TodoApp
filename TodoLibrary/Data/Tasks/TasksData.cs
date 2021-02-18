using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Tasks
{
    public class TasksData : ITasksData
    {
        private readonly ApplicationDbContext _db;

        public TasksData(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateTask(TaskModel task)
        {
            _db.Tasks.Add(task);
            _db.SaveChanges();
        }
        
        public List<TaskModel> GetTasks()
        {
            return _db.Tasks
                .Include(c => c.CategoryModel)
                .ToList();
        }
    }
}