using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Tasks
{
    public class TasksData : ITasksData
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Creates and initialize database connection
        /// </summary>
        /// <param name="db">Injected via dependency injection</param>
        public TasksData(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateTask(TaskModel task)
        {
            _db.Tasks.Add(task);
            _db.SaveChanges();
        }

        public TaskModel GetTask(int id)
        {
            return _db.Tasks.Where(x => x.Id == id)
                .Include(x => x.CategoryModel)
                .First();
        }

        public List<TaskModel> GetTasks()
        {
            return _db.Tasks
                .Include(c => c.CategoryModel)
                .ToList();
        }
    }
}