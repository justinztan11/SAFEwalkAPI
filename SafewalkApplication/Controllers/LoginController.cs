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
        public LoginController(ILoginRepository loginRepository, IUserRepository userRepository)
        {
            _loginRepository = loginRepository;
            _userRepository = userRepository;
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
        public async Task<IActionResult> VerifyUser([FromRoute] string email, [FromHeader] string? password)
        {
            if (password == null)
            {
                if (await _userRepository.Exists(email))
                {
                    return Conflict();
                }
                return Ok();
            }
            else
            {
                var user = await _userRepository.Get(email);
                if (user == null || user.Password != password)
                {
                    return Conflict();
                }
                return Ok();
            }

        }
    }
}