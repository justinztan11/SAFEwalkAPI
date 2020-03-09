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
        // Unauthorized Fields: Id, Password, Token
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser([FromHeader] string? token, [FromRoute] string? email, [FromHeader] bool? isUser)
        {
            if (token == null || email == null || isUser == null)
            {
                return BadRequest();
            }

            // if user and not authenticated
            if ((bool)isUser && !await _userRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }
            // is safewalker and not authenticated
            else if ((bool)!isUser && !await _safewalkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }

            // TODO: handle null user error
            var user = await _userRepository.Get(email);
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        // PUT: api/Users/{email}
        // Authorization: User
        // Unauthorized Fields: Id, Password, Token
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUser([FromHeader] string token, [FromRoute] string email, [FromBody] User user)
        {
            if (!(await _userRepository.Authenticated(token, email)))
            {
                return Unauthorized();
            }

            // TODO populate old model into new model



            return Ok(await _userRepository.Update(email, user));




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

            if (token == null || email == null)
            {
                return BadRequest();
            }

            if (await _userRepository.Exists(email))
            {
                return Ok(await _userRepository.Get(email));
            }
            
            return NotFound();
        }
    }
}
