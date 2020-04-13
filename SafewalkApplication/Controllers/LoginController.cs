using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafewalkApplication.Contracts;

#nullable enable
namespace SafewalkApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISafewalkerRepository _safewalkerRepository;
        public LoginController(ILoginRepository loginRepository, IUserRepository userRepository, ISafewalkerRepository safewalkerRepository)
        {
            _loginRepository = loginRepository;
            _userRepository = userRepository;
            _safewalkerRepository = safewalkerRepository;
        }

        // GET: api/Login
        [HttpGet]
        public async Task<ActionResult<string>> GetLogin([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isUser)
        {
            string token;
            if (isUser)
            {
                token = await _loginRepository.GetUser(email, password);
            }
            else
            {
                token = await _loginRepository.GetWalker(email, password);
            }

            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }

        // GET: api/Login/{email}
        [HttpGet("{email}")]
        public async Task<IActionResult> VerifyEmail([FromRoute] string email)
        {
            if (await _userRepository.Exists(email))
            {
                return Conflict();
            }
            return Ok();
        }


        // GET: api/Login/{email}/PasswordVerify
        [HttpGet("{email}/PasswordVerify")]
        public async Task<IActionResult> VerifyPassword([FromRoute] string email, [FromHeader] string password, [FromHeader] bool isUser)
        {
            if (isUser) 
            {
                var user = await _userRepository.Get(email);
                if (user == null || user.Password != password)
                {
                    return Unauthorized();
                }
                return Ok();
            }
            else
            {
                var safewalker = await _safewalkerRepository.Get(email);
                if (safewalker == null || safewalker.Password != password)
                {
                    return Unauthorized();
                }
                return Ok();
            }
        }
    }
}