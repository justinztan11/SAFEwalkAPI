using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafewalkApplication.Contracts;
using SafewalkApplication.Models;

namespace SafewalkApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser([FromHeader] string token, [FromRoute] string email)
        {
            // if user is signed in and authenticated
            if (!(await UserAuthenticated(email, token))) 
            {
                // not authenticated, return error authentication message
            }

            return Ok();
             
        }

        // PUT: api/Users/{email}
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUser([FromHeader] string token, [FromRoute] string email, [FromBody] User user)
        {
            return Ok();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            return Ok();
        }

        // DELETE: api/Users/{email}
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

        // checks if user is authenticated/signed-in
        private async Task<bool> UserAuthenticated(string token, string email)
        {
            return await _userRepository.Authenticated(token, email);
        }
    }
}
