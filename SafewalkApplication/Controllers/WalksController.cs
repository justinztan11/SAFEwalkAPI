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
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISafewalkerRepository _safewalkerRepository;

        public WalksController(IWalkRepository walkRepository, IUserRepository userRepository, ISafewalkerRepository safewalkerRepository)
        {
            _walkRepository = walkRepository;
            _safewalkerRepository = safewalkerRepository;
            _userRepository = userRepository;
        }

        // GET: api/Walks
        // Authorization: Safewalker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Walk>>> GetWalks([FromHeader] string? token)
        {
            if (token == null) // if no token
            {
                return BadRequest();
            }

            // if not signed in nor authenticated
            if (!(await _safewalkerRepository.Authenticated(token)))
            {
                return Unauthorized();
            }

            IEnumerable<Walk> walkList = _walkRepository.GetAll();

            // if there are no walks 
            if (!walkList.Any<Walk>())
            {
                return NotFound();
            }
            
            return Ok(new ObjectResult(walkList));
        }

        // GET: api/Walks/{id}
        // Authorization: User, Safewalker
        [HttpGet("{email}")]
        public async Task<ActionResult<Walk>> GetWalk([FromHeader] string token, [FromRoute] string id, [FromHeader] bool isUser)
        {
            return Ok();
        }

        // PUT: api/Walks/{id}
        // Authorization: User - can modify status, Safewalker - can modify email and status
        [HttpPut("{email}")]
        public async Task<IActionResult> PutWalk([FromHeader] string token, [FromRoute] string id, [FromHeader] bool isUser, [FromBody] Walk walk)
        {
            return Ok();
        }

        // POST: api/Walk
        // Authorization: User
        [HttpPost]
        public async Task<ActionResult<Walk>> PostWalk([FromHeader] string token, [FromBody] Walk walk)
        {
            return Ok();
        }

        // checks if walk is pending or ongoing
        private bool WalkExists(string id)
        {
            return true;
        }
    }
}
