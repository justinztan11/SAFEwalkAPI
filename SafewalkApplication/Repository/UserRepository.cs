using SafewalkApplication.Contracts;
using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SafewalkDatabaseContext _context;

        public UserRepository(SafewalkDatabaseContext context)
        {
            _context = context;
        }

        public Task<User> Add(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(string token, string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(string token, string email, User user)
        {
            throw new NotImplementedException();
        }
    }
}
