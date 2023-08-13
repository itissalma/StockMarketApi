using Backend.Models;
using System.Collections.Generic;

namespace Backend.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(string id);
        User GetUserByPassword(string password);
        User ValidateUserCredentials(string userName, string password);
        User GetUserByEmailOrNatId(string email, string natId);
        User GetUserByUserName(string userName); // Add this method
        User Register(User account);
    }
}
