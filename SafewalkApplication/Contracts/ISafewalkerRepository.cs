using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Contracts
{
    public interface ISafewalkerRepository
    {
        Task<Safewalker> Get(string email);

        Task<User> Update(string email, Safewalker safewalker);
        
        Task<bool> Exists(string email);

        Task<bool> Authenticated(string token, string email);
    }
}
