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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;


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
                    username = user.Username,
                    profilePicturePath = user.ProfilePicture,
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
                    username = user.Username,
                    profilePicturePath = user.ProfilePicture,
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }

    [Authorize]
    [Route("api/updateProfilePicture")]
    [ApiController]
    public class UserProfilePictureController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly UserService _userService;

        public UserProfilePictureController(IWebHostEnvironment env, UserService userService)
        {
            _env = env;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file uploaded." });

            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"user_{userId}{extension}";
            var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "profile-pictures");
            var filePath = Path.Combine(folderPath, fileName);
            var relativePath = $"/profile-pictures/{fileName}";

            // Asigură-te că folderul există
            Directory.CreateDirectory(folderPath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            try
            {
                _userService.updateProfilePicture(userId, relativePath); // metoda din UserService
                return Ok(new { message = "Profile picture uploaded.", path = relativePath });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }

}