using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("password/{password}")]
        public IActionResult GetUserByPassword(string password)
        {
            var user = _userService.GetUserByPassword(password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }

            var tokenString = _userService.Login(user, _configuration["Jwt:Key"], _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"]);

            if (!string.IsNullOrEmpty(tokenString))
            {
                return Ok(new JWTTokenResponse
                {
                    Token = tokenString
                });
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User account)
        {
            var result = _userService.Register(account);
            if (result == null)
            {
                return BadRequest("User already exists!!!");
            }
            return Ok(result);
        }

        [HttpGet("username/{userName}")]
        public IActionResult GetUserByUserName(string userName)
        {
            var user = _userService.GetUserByUserName(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
    }
    }
}
