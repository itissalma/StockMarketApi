using Backend.Models;

namespace Backend.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserById(string id);
        User GetUserByPassword(string password);
        string Login(Login user, string jwtKey, string jwtIssuer, string jwtAudience);
        User Register(User account);
        User GetUserByUserName(string userName);
    }
}
