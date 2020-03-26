using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafewalkApplication.Contracts;
using SafewalkApplication.Helpers;
using SafewalkApplication.Models;

#nullable enable
namespace SafewalkApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISafewalkerRepository _safewalkerRepository;

        public UsersController(IUserRepository userRepository, ISafewalkerRepository safewalkerRepository)
        {
            _userRepository = userRepository;
            _safewalkerRepository = safewalkerRepository;
        }

        // GET: api/Users/{email}
        // Authorization: User, Safewalker
        // Unauthorized Fields: Id, Password, Token
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser([FromHeader] string token, [FromRoute] string email, [FromHeader] bool isUser)
        {
            // if user and not authenticated
            if (isUser && !await _userRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }
            // is safewalker and not authenticated
            else if (!isUser && !await _safewalkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }

            var user = await _userRepository.Get(email);
            user.WithoutPrivateInfo();
            return Ok(user);
        }

        // PUT: api/Users/{email}
        // Authorization: User
        // Unauthorized Fields: Id, Password, Token
        [HttpPut("{email}")]
        public async Task<ActionResult<User>> PutUser([FromHeader] string token, [FromRoute] string email, [FromBody] User user)
        {
            if (!(await _userRepository.Authenticated(token, email)))
            {
                return Unauthorized();
            }

            var oldUser = await _userRepository.Get(email);
            oldUser.MapFields(user);
            var newUser = await _userRepository.Update(oldUser);
            newUser.WithoutPrivateInfo();
            return Ok(newUser);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            if (await _userRepository.Exists(user.Email)) 
            {
                return Conflict(user);
            }

            Guid guid = Guid.NewGuid();
            user.Id = guid.ToString();
            await _userRepository.Add(user);
            user.WithoutPrivateInfo();
            return Ok(user);
        }

        // DELETE: api/Users/{email}
        // Authorization: User
        [HttpDelete("{email}")]
        public async Task<ActionResult<User>> DeleteUser([FromHeader] string token, [FromRoute] string email)
        {
            if (!(await _userRepository.Authenticated(token, email)))
            {
                return Unauthorized();
            }

            if (!await _userRepository.Exists(email))
            {
                return NotFound();
            }

            var user = await _userRepository.Get(email);
            user.WithoutPrivateInfo();
            await _userRepository.Delete(email);
            return Ok(user);
        }

        // POST: api/Users/{email}
        [HttpPost("{email}")]
        public async Task<ActionResult<User>> PostLogOutUser([FromHeader] string token, [FromRoute] string email)
        {
            if (!(await _userRepository.Authenticated(token, email)))
            {
                return Unauthorized();
            }

            if (!await _userRepository.Exists(email))
            {
                return NotFound();
            }

            var oldUser = await _userRepository.Get(email);
            oldUser.Token = null;
            var newUser = await _userRepository.Update(oldUser);
            newUser.WithoutPrivateInfo();
            return Ok(newUser);
        }
    }
}
