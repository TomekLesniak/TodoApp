using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Users
{
    public interface IUsersData
    {
        void CreateUser(UserModel user);
        UserModel GetUser(int id);
        List<UserModel> GetUsers();
        void RemoveUser(UserModel user);
    }
}