using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafewalkApplication.Contracts;
using SafewalkApplication.Helpers;
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
        // Unauthorized Fields: Id, Password, Token
        [HttpGet("{email}")]
        public async Task<ActionResult<Safewalker>> GetSafewalker([FromHeader] string? token, [FromRoute] string? email, [FromHeader] bool? isUser)
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

            var safewalker = await _safewalkerRepository.Get(email);
            if (safewalker == null)
            {
                return NotFound();
            }
            return Ok(safewalker);
        }

        // PUT: api/Safewalkers/{email}
        // Authorization: Safewalker
        [HttpPut("{email}")]
        public async Task<IActionResult> PutSafewalker([FromHeader] string token, [FromRoute] string email, [FromBody] Safewalker safewalker)
        {
            if (!(await _safewalkerRepository.Authenticated(token, email)))
            {
                return Unauthorized();
            }
            var oldWalker = await _safewalkerRepository.Get(email);
            if (oldWalker == null)
            {
                return NotFound();
            }
            oldWalker.MapFields(safewalker);

            return Ok(await _safewalkerRepository.Update(oldWalker));
        }

    }
}
