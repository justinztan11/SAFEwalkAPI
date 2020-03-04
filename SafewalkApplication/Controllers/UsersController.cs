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
        private bool UserExists(string email)
        {
            return true;
        }
    }
}
