using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafewalkApplication.Contracts;
using SafewalkApplication.Helpers;

namespace SafewalkApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISafewalkerRepository _safewalkerRepository;

        public LogoutController(IUserRepository userRepository, ISafewalkerRepository safewalkerRepository)
        {
            _userRepository = userRepository;
            _safewalkerRepository = safewalkerRepository;
        }

        // PUT: api/Logout
        [HttpPut]
        public async Task<IActionResult> PutLogout([FromHeader] string token, [FromHeader] string email, [FromHeader] bool isUser)
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

            if (isUser)
            {
                var user = await _userRepository.Get(email);
                if (user == null)
                {
                    return NotFound();
                }

                user.WithoutTempAuth();
                await _userRepository.Update(user);   
            }
            else
            {
                var walker = await _safewalkerRepository.Get(email);
                if (walker == null)
                {
                    return NotFound();
                }

                walker.WithoutTempAuth();
                await _safewalkerRepository.Update(walker);
            }

            return Ok();
        }
    }
}