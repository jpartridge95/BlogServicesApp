using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserActions _service;

        public UsersController(IUserActions service)
        {
            _service = service;
        }

        [HttpGet("byid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetByID(int id)
        {
            var user = await _service.GetByID(id);

            if (user == null)
                return NotFound();
            

            return user;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _service.GetAllUsers();

            if (users == null)
                return NotFound();
            

            return users;
        }

        [HttpGet("byemail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            var user = await _service.GetByEmail(email);

            if (user == null)
                return NotFound();
            

            return user;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateUser(User user)
        {
            if (await _service.CreateUser(user))
                return NoContent();
            

            return NotFound();
        }

        [HttpDelete("byid")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            if (await _service.DeleteUser(userId)) 
                return NoContent();

            return NotFound();
        }

        [HttpPatch("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUser(int userId, User newData)
        {
            if (await _service.UpdateUser(userId, newData)) 
                return NoContent();

            return NotFound();
        }

    }
}
