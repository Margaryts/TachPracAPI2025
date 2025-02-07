using Microsoft.AspNetCore.Mvc;
using TachPracAPI.Models;

namespace TachPracAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new User { Id = 2, Name = "Alice", Email = "alice@example.com" },
            new User { Id = 3, Name = "Margo Pylyp", Email = "margo@example.com" }
        };



        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }
        [HttpGet("{id}")]
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