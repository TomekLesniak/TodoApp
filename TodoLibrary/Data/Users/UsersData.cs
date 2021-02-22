using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Users
{
    public class UsersData : IUsersData
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Creates and initialize database connection
        /// </summary>
        /// <param name="db">Injected via dependency injection</param>
        public UsersData(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void CreateUser(UserModel user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public UserModel GetUser(int id)
        {
            return _db.Users.First(x => x.Id == id);
        }

        public List<UserModel> GetUsers()
        {
            return _db.Users.ToList();
        }

        public void RemoveUser(UserModel user)
        {
            _db.Remove(user);
            _db.SaveChanges();
        }
    }
}
