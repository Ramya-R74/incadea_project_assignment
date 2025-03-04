using Microsoft.AspNetCore.Mvc;
using Mini_ERP_System.Data;

namespace Mini_ERP_System.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new
            {
                message = "Login successful",
                userId = user.UserId,
                userName = user.UserName,
                role = user.Role
            });
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
