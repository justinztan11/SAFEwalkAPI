using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Contracts
{
    public interface ILoginRepository
    {
        Task<string> Get(string email, string password);

        Task<bool> Exists(string email);
    }
}
