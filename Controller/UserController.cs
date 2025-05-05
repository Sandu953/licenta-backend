using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Model;



namespace Backend.Controller
{
    public class UserRequestRegister
    {
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
    }

    public class UserRequestLogin
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserRequestLogin loginData)
        {

            Console.WriteLine(loginData.email);
            if (_userService.Login(loginData.email, loginData.password))
            {
                var user = _userService.FindUserByEmail(loginData.email);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", user.Id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gT7kP9lZ6bU1wR3xM2cQvNp8YsA7eFdT"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "car_app",
                    audience: "car_app",
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: creds
                );

                return Ok(new { 
                    message = "Login permitted!", 
                    userId = user.Id, 
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            else
            {
                return BadRequest(new { message = "Email or password are wrong" });
            }
        }

        [HttpPost("register")]
        public IActionResult AddUser([FromBody] UserRequestRegister request)
        {
            try
            {
                var user = _userService.Save(request.email, request.password, request.username);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", user.Id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gT7kP9lZ6bU1wR3xM2cQvNp8YsA7eFdT"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "car_app",
                    audience: "car_app",
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    message = "Register permitted!",
                    userId = user.Id,
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch(Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
