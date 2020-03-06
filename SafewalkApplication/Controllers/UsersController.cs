using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafewalkApplication.Contracts;
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
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser([FromHeader] string? token, [FromRoute] string? email, [FromHeader] bool? isUser)
        {
            if (token == null || email == null || isUser == null)
            {
                return BadRequest();
            }

            if ((bool)isUser) // if User
            {
                // if not signed in and authenticated
                if (!(await _userRepository.Authenticated(token)))
                {
                    // TODO: return error response
                }
            }
            else // if Safewalker
            {
                // if not signed in nor authenticated
                if (!(await _safewalkerRepository.Authenticated(token)))
                {
                    // TODO: return error response
                }
            }

            // TODO: handle null user error
            var user = await _userRepository.Get(email);

            return Ok(user);
             
        }

        // PUT: api/Users/{email}
        // Authorization: User
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUser([FromHeader] string token, [FromRoute] string email, [FromBody] User user)
        {
            return Ok();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            Guid guid = Guid.NewGuid();
            user.Id = guid.ToString();
            await _userRepository.Add(user);
            return Ok(user);
        }

        // DELETE: api/Users/{email}
        // Authorization: User
        [HttpDelete("{email}")]
        public async Task<ActionResult<User>> DeleteUser([FromHeader] string token, [FromRoute] string email)
        {
            return Ok();
        }

        // checks if user exists
        private async Task<bool> UserExists(string email)
        {
            return true;
        }
    }
}
