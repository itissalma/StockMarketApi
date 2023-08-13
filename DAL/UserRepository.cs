using System.Collections.Generic;
using System.Linq;
using Backend.Models;

namespace Backend.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly StockMarketContext _context;

        public UserRepository(StockMarketContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.NatId == id);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public User GetUserByPassword(string password)
        {
            return _context.Users.FirstOrDefault(u => u.Password == password);
        }

        public User ValidateUserCredentials(string userName, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == userName && u.Password == password);
        }

        public User GetUserByEmailOrNatId(string email, string natId)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email || u.NatId == natId);
        }

        public User Register(User account)
        {
            _context.Users.Add(account);
            _context.SaveChanges();
            return account;
        }
    }
}