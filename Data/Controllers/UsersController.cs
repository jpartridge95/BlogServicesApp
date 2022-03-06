using Data.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Data.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly UserAccess _access;

        public UsersController(ILogger<UsersController> logger, UserAccess access)
        {
            _logger = logger;
            _access = access;
        }

        [HttpGet("byId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetById(int userId)
        {
            User user = await _access.GetByID(userId);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("byemail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            User user = await _access.GetByEmail(email);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<User>>> SecretHiddenEndpoint()
        {
            List<User> users = await _access.GetAllUsers();
            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpDelete("ById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteById(int userId)
        {
            if (await _access.DeleteUser(userId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> CreateUser(User user)
        {
            if (await _access.CreateUser(user))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("edit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> EditUserInfo(int userId, User user)
        {
            if (await _access.UpdateUser(userId, user))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}