using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafewalkApplication.Contracts;

namespace SafewalkApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        // GET: api/Login
        [HttpGet("{email}")]
        public async Task<ActionResult<string>> GetUser([FromHeader] string email, [FromHeader] string password)
        {
            return Ok();
        }

        // checks if user exists
        private bool UserExists(string email)
        {
            return true;
        }

    }
}