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

        public UsersData(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void CreateUser(UserModel user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public List<UserModel> GetUsers()
        {
            return _db.Users.ToList();
        }
    }
}
