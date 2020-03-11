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
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        // GET: api/Login
        [HttpGet]
        public async Task<ActionResult<string>> GetLogin([FromHeader] string email, [FromHeader] string password, [FromHeader] bool isUser)
        {
            IPerson? person = null;
            if (isUser)
            {
                person = await _loginRepository.GetUser(email, password);
            } 
            else
            {
                person = await _loginRepository.GetWalker(email, password);
            }

            if (person != null)
            {
                var token = person.Token;
                ////Save token in session object
                //HttpContext.Session.SetString("JWToken", token);
                return Ok(token);
            } else
            {
                return NotFound();
            }
   
        }

    }
}