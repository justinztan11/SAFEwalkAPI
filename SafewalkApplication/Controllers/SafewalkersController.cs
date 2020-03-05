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
    public class SafewalkersController : ControllerBase
    {
        private readonly ISafewalkerRepository _safewalkerRepository;
        private readonly IUserRepository _userRepository;

        public SafewalkersController(ISafewalkerRepository safewalkerRepository, IUserRepository userRepository)
        {
            _safewalkerRepository = safewalkerRepository;
            _userRepository = userRepository;
        }

        // GET: api/Safewalkers/{email}
        // Authorization: User, Safewalker
        [HttpGet("{email}")]
        public async Task<ActionResult<Safewalker>> GetSafewalker([FromHeader] string? token, [FromRoute] string? email, [FromHeader] bool? isUser)
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
                    // return error authentication message
                }
            }
            else // if Safewalker
            {
                // if not signed in and authenticated
                if (!(await _safewalkerRepository.Authenticated(token)))
                {
                    // return error authentication message
                }
            }

            return Ok();
        }

        // PUT: api/Safewalkers/{email}
        // Authorization: Safewalker
        [HttpPut("{email}")]
        public async Task<IActionResult> PutSafewalker([FromHeader] string token, [FromRoute] string email, [FromBody] Safewalker safewalker)
        {
            return Ok();
        }

        // check if safewalker exists
        private bool SafewalkerExists(string email)
        {
            return true;
        }
    }
}
