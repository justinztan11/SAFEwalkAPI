using SafewalkApplication.Contracts;
using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SafewalkDatabaseContext _context;

        public LoginRepository(SafewalkDatabaseContext context)
        {
            _context = context;
        }

        public Task<bool> Exists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> Get(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
