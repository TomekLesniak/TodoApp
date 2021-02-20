using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using TodoLibrary.Models;

namespace TodoLibrary.Data.UserTasks
{
    public class UserTasksData : IUserTasksData
    {
        private readonly ApplicationDbContext _db;

        public UserTasksData(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void CreateUserTask(UserTasksModel task)
        {
            _db.UserTasks.Add(task);
            _db.SaveChanges();
        }

        public void CompleteUserTask(int taskId)
        {
            var task = _db.UserTasks.Find(taskId);
            task.IsFinished = true;
            _db.SaveChanges();
        }


        public List<UserTasksModel> GetUserTasks(int userId)
        {
            var output = _db.UserTasks
                .Where(u => u.UserId == userId)
                .Include(t => t.Task)
                .ThenInclude(t => t.CategoryModel)
                .ToList();

            return output;
        }
    }
}
