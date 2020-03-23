using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafewalkApplication.Contracts;
using SafewalkApplication.Helpers;
using SafewalkApplication.Models;

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
        public async Task<ActionResult<Safewalker>> GetSafewalker([FromHeader] string token, [FromRoute] string email, [FromHeader] bool isUser)
        {
            // if user and not authenticated
            if (isUser && !await _userRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }
            // is safewalker and not authenticated
            else if (!isUser && !await _safewalkerRepository.Authenticated(token, email))
            {
                return Unauthorized();
            }

            var walker = await _safewalkerRepository.Get(email);
            walker.WithoutPrivateInfo();
            return Ok(walker);
        }

        // PUT: api/Safewalkers/{email}
        // Authorization: Safewalker
        [HttpPut("{email}")]
        public async Task<IActionResult> PutSafewalker([FromHeader] string token, [FromRoute] string email, [FromBody] Safewalker walker)
        {
            // if safewalker not authenticated
            if (!(await _safewalkerRepository.Authenticated(token, email)))
            {
                return Unauthorized();
            }

            var oldWalker = await _safewalkerRepository.Get(email);
            if (oldWalker == null)
            {
                return NotFound();
            }
            
            oldWalker.MapFields(walker);
            var newWalker = await _safewalkerRepository.Update(oldWalker);
            var copyWalker = newWalker.DeepClone().WithoutPrivateInfo();
            return Ok(copyWalker);
        }

    }
}
