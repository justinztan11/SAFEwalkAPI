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
    public class SafewalkersController : ControllerBase
    {
        private readonly ISafewalkerRepository _safewalkerRepository;

        public SafewalkersController(ISafewalkerRepository safewalkerRepository)
        {
            _safewalkerRepository = safewalkerRepository;
        }

        // GET: api/Safewalkers/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<Safewalker>> GetSafewalker([FromHeader] string token, [FromRoute] string email)
        {
            return Ok();
        }

        // PUT: api/Safewalkers/{email}
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

        // checks if Safewalker is authenticated/signed-in
        private async Task<bool> UserAuthenticated(string token, string email)
        {
            return await _safewalkerRepository.Authenticated(token, email);
        }
    }
}
