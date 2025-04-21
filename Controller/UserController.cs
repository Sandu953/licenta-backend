using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



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
                return Ok(new { message = "Login permitted!" });
            }
            else
            {
                return ValidationProblem(new ValidationProblemDetails().Detail = "email or password are wrong");
            }
        }

        [HttpPost("register")]
        public IActionResult AddUser([FromBody] UserRequestRegister request)
        {
            try
            {
                _userService.Save(request.email, request.password, request.username);
                return Ok(new { message = "User registered!" });
            }
            catch
            {
                return ValidationProblem(new ValidationProblemDetails().Detail = "email/username already in use");
            }
        }
    }
}
