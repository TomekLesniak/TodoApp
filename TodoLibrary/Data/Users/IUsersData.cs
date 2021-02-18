using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Users
{
    public interface IUsersData
    {
        void CreateUser(UserModel user);
        List<UserModel> GetUsers();
        void RemoveUser(UserModel user);
    }
}