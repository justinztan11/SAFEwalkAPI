using SafewalkApplication.Contracts;
using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public Task<User> Get(string token)
        {
            throw new NotImplementedException();
        }
    }
}
