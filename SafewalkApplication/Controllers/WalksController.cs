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
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;

        public WalksController(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
        }

        // GET: api/Walks
        // Authorization: Safewalker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Walk>>> GetWalks([FromHeader] string token)
        {
            return Ok();
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
