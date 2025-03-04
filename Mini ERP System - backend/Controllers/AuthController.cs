using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mini_ERP_System.Data;
using Mini_ERP_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Mini_ERP_System.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        private readonly string _apiKey;

        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
            _apiKey = _config["ApiKey"]; // Read API key from appsettings.json
        }

        // AUTHENTICATE ADMIN USING API KEY
        [HttpPost]
        [Route("AdminLogin/")]
        public IActionResult AdminLogin([FromHeader] string apiKey)
        {
            if (apiKey != _apiKey)
                return Unauthorized(new { message = "Invalid API Key" });

            return Ok(new { message = "Admin authenticated successfully" });
        }

        // ADD A NEW USER (Only Admin Can Access)
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromHeader] string apiKey, [FromBody] User user)
        {
            if (apiKey != _apiKey)
                return Unauthorized(new { message = "Invalid API Key" });

            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
                return BadRequest(new { message = "Username and password are required" });

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User added successfully" });
        }


        // GET USER PROFILE BY USERNAME (Only Admin Can Access)
        [HttpGet]
        [Route("GetUser/{username}")]
        public async Task<IActionResult> GetUser([FromHeader] string apiKey, string username)
        {
            if (apiKey != _apiKey)
                return Unauthorized(new { message = "Invalid API Key" });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return NotFound(new { message = "User not found" });

            return Ok(new { user.UserId, user.UserName, user.Role });
        }

        // UPDATE USER DETAILS (Only Admin Can Access)
        [HttpPut]
        [Route("UpdateUser/{username}")]
        public async Task<IActionResult> UpdateUser([FromHeader] string apiKey, string username, [FromBody] User updatedUser)
        {
            if (apiKey != _apiKey)
                return Unauthorized(new { message = "Invalid API Key" });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return NotFound(new { message = "User not found" });

            user.UserName = updatedUser.UserName ?? user.UserName;
            user.Password = updatedUser.Password ?? user.Password;
            user.Role = updatedUser.Role ?? user.Role;

            await _context.SaveChangesAsync();
            return Ok(new { message = "User updated successfully" });
        }

        // DELETE USER (Only Admin Can Access)
        [HttpDelete]
        [Route("DeleteUser/{username}")]
        public async Task<IActionResult> DeleteUser([FromHeader] string apiKey, string username)
        {
            if (apiKey != _apiKey)
                return Unauthorized(new { message = "Invalid API Key" });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return NotFound(new { message = "User not found" });

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully" });
        }

        // GET ALL USERS (Only Admin Can Access)
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromHeader] string apiKey)
        {
            if (apiKey != _apiKey)
                return Unauthorized(new { message = "Invalid API Key" });

            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }
}
