using System.Collections.Generic;
using TodoLibrary.Models;

namespace TodoLibrary.Data.Users
{
    public interface IUsersData
    {
        /// <summary>
        /// Saves a new user into database
        /// </summary>
        /// <param name="user">The user information</param>
        void CreateUser(UserModel user);
        
        /// <summary>
        /// Get user by id from database
        /// </summary>
        /// <param name="id">Unique identifier for user</param>
        /// <returns></returns>
        UserModel GetUser(int id);
        
        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns>List of all available users; empty list if no records found</returns>
        List<UserModel> GetUsers();
        
        /// <summary>
        /// Removes user from the database
        /// </summary>
        /// <param name="user">Information of the user</param>
        void RemoveUser(UserModel user);
    }
}