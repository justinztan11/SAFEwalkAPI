using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafewalkApplication.Contracts;

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
        public async Task<ActionResult<string>> GetToken([FromHeader] string? email, [FromHeader] string? password, [FromHeader] bool isUser)
        {
            if (email == null || password == null)
            {
                return BadRequest();
            } 

            var user = await _loginRepository.Get(email, password, isUser);
            if (user != null)
            {
                var token = user.Token;
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