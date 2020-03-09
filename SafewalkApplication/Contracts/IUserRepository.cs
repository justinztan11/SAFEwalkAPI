using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Contracts
{
    public interface IUserRepository
    {
        Task<User> Get(string email);

        Task<User> Add(User user);

        Task<User> Update(User user);

        Task<bool> Exists(string email);

        Task<bool> Authenticated(string token, string email);

        Task<User> Delete(string email);
    }
}
