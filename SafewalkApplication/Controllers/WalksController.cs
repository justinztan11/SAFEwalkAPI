using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafewalkApplication.Contracts;
using SafewalkApplication.Models;
using SafewalkApplication.Helpers;

namespace SafewalkApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IUserRepository _walkerRepository;
        private readonly ISafewalkerRepository _safewalkerRepository;

        public WalksController(IWalkRepository walkRepository, IUserRepository userRepository, ISafewalkerRepository safewalkerRepository)
        {
            _walkRepository = walkRepository;
            _safewalkerRepository = safewalkerRepository;
            _walkerRepository = userRepository;
        }

        // GET: api/Walks
        // Authorization: Safewalker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Walk>>> GetWalks([FromHeader] string token, [FromHeader] string email)
        {
            // if not signed in and authenticated
            if (!(await _safewalkerRepository.Authenticated(token, email)))
            {
                return Unauthorized();
            }

            IEnumerable<Walk> walkList = _walkRepository.GetAll();
            return Ok(walkList);
        }

        // GET: api/Walks/{id}
        // Authorization: User, Safewalker
        [HttpGet("{id}")]
        public async Task<ActionResult<Walk>> GetWalk([FromHeader] string token, [FromHeader] string email, 
            [FromRoute] string id, [FromHeader] bool isUser)
        {
            // if user and not authenticated
            if (isUser && !await _walkerRepository.Authenticated(token, email)) 
            {
                return Unauthorized();
            }
            // is safewalker and not authenticated
            else if (!isUser && !await _safewalkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }
            
            var walk = await _walkRepository.Get(id);
            if (walk == null)
            {
                return NotFound();
            }

            // Check if person is part of the walk
            if (isUser && walk.UserEmail != email)
            {
                return Unauthorized();
            }
            else if (!isUser && walk.WalkerEmail != email)
            {
                return Unauthorized();
            }

            return Ok(walk);
        }

        // GET: api/Walks/{id}/status
        // Authorization: User, Safewalker
        [HttpGet("{id}/status")]
        public async Task<ActionResult<int>> GetWalkStatus([FromHeader] string token, [FromHeader] string email,
            [FromRoute] string id, [FromHeader] bool isUser)
        {
            // if user and not authenticated
            if (isUser && !await _walkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }
            // is safewalker and not authenticated
            else if (!isUser && !await _safewalkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }

            var walk = await _walkRepository.Get(id);
            if (walk == null)
            {
                return NotFound();
            }

            var status = walk.Status;
            if (status == null)
            {
                return NotFound();
            }

            // Check if person is part of the walk
            if (isUser && walk.UserEmail != email)
            {
                return Unauthorized();
            }

            return Ok(status);
        }

        // GET: api/Walks/{id}/location
        // Authorization: User, Safewalker
        [HttpGet("{id}/location")]
        public async Task<ActionResult<Walk>> GetWalkerLocation([FromHeader] string token, [FromHeader] string email,
            [FromRoute] string id, [FromHeader] bool isUser)
        {
            // if user and not authenticated
            if (isUser && !await _walkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }
            // is safewalker and not authenticated
            else if (!isUser && !await _safewalkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }

            var walk = await _walkRepository.Get(id);
            if (walk == null)
            {
                return NotFound();
            }

            var currLat = walk.WalkerCurrLat;
            var currLng = walk.WalkerCurrLng;
            if (currLat == null || currLng == null)
            {
                return NotFound();
            }

            // Check if person is part of the walk
            if (isUser && walk.UserEmail != email)
            {
                return Unauthorized();
            }
            else if (!isUser && walk.WalkerEmail != email)
            {
                return Unauthorized();
            }

            var walkerLocation = new { lat = currLat, lng = currLng };
            return Ok(walkerLocation);
        }

        // PUT: api/Walks/{id}
        // Authorization: User, Safewalker
        // User Field Access: Status
        // Safewalker Field Access: Status, WalkerEmail
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWalk([FromHeader] string token, [FromHeader] string email,
            [FromRoute] string id, [FromHeader] bool isUser, [FromBody] Walk walk)
        {
            // if user and not authenticated
            if (isUser && !await _walkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }
            // is safewalker and not authenticated
            else if (!isUser && !await _safewalkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }

            var oldWalk = await _walkRepository.Get(id);
            if (oldWalk == null)
            {
                return NotFound();
            }

            // Check if person is part of the walk
            if (isUser && oldWalk.UserEmail != email)
            {
                return Unauthorized();
            }

            // map fields from input walk to existing walk
            if (isUser)
            {
                oldWalk.UserMapFields(walk);
            } 
            else
            {
                oldWalk.WalkerEmail = email;
                oldWalk.WalkerMapFields(walk);
            }

            var newWalk = await _walkRepository.Update(oldWalk);
            return Ok(newWalk);
        }

        // POST: api/Walks
        // Authorization: User
        // Field Access: 
        [HttpPost]
        public async Task<ActionResult<Walk>> PostWalk([FromHeader] string token, [FromHeader] string email, [FromBody] Walk walk)
        {
            // if user not authenticated
            if (!await _walkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }

            if (await _walkRepository.Exists(email))
            {
                return Conflict();
            }

            Guid guid = Guid.NewGuid();
            walk.Id = guid.ToString();
            walk.Status = 0;
            walk.UserEmail = email;
            await _walkRepository.Add(walk);
            walk.WithoutWalkerInfo();
            return Ok(walk);
        }

        // DELETE: api/Walks/{id}
        // Authorization: User
        [HttpDelete("{id}")]
        public async Task<ActionResult<Walk>> DeleteWalk([FromHeader] string token, [FromHeader] string email, [FromRoute] string id, [FromHeader] bool isUser)
        {
            // if user and not authenticated
            if (isUser && !await _walkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }
            // is safewalker and not authenticated
            else if (!isUser && !await _safewalkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }

            var oldWalk = await _walkRepository.Get(id);
            if (oldWalk == null)
            {
                return NotFound();
            }

            // Check if person is part of the walk
            if (isUser && oldWalk.UserEmail != email)
            {
                return Unauthorized();
            }

            var walk = await _walkRepository.Delete(id);
            walk.WithoutWalkerInfo();
            return Ok(walk);
        }

    }
}
