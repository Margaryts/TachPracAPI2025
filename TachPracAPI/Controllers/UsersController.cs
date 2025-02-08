using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TachPracAPI.Models;

namespace TachPracAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize] 
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com", Role = "User" },
            new User { Id = 2, Name = "Alice", Email = "alice@example.com", Role = "User" },
            new User { Id = 3, Name = "Admin", Email = "admin@example.com", Role = "Admin" }
        };

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")] 
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (users.Any(u => u.Id == user.Id))
            {
                return BadRequest(new { message = "User with this ID already exists" });
            }
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")] 
        public ActionResult<User> UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            users.Remove(user);
            return NoContent();
        }
    }
}
