using System;
using System.Linq;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Backend.Data.Repositories;
using System.Security.Claims;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetUserById(string id)
        {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByPassword(string password)
        {
            return _userRepository.GetUserByPassword(password);
        }

        public string Login(Login user, string jwtKey, string jwtIssuer, string jwtAudience)
        {
            var validUser = _userRepository.ValidateUserCredentials(user.UserName, user.Password);

            if (validUser != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return tokenString;
            }

            return null;
        }

        public User Register(User account)
        {
            var existingUser = _userRepository.GetUserByEmailOrNatId(account.Email, account.NatId);

            if (existingUser != null)
            {
                return null;
            }

            return _userRepository.Register(account);
        }

        public User GetUserByUserName(string userName)
        {
            return _userRepository.GetUserByUserName(userName);
        }
    }
}
