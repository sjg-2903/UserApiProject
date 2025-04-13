using Microsoft.AspNetCore.Mvc;
using UserApiProject.Models;
using UserApiProject.Services;

namespace UserApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_userService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return user == null ? NotFound("User not found") : Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
                return BadRequest("Name is required.");

            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
                return BadRequest("A valid email is required.");

            var created = _userService.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            if (!_userService.Update(id, user))
                return NotFound("User not found");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_userService.Delete(id))
                return NotFound("User not found");

            return NoContent();
        }
    }
}
