using Microsoft.EntityFrameworkCore;
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

        public Task<User> Get(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(string email, User user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Exists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Authenticated(string token, string email)
        {
            return _context.User.AnyAsync(m => m.Email == email && m.Token == token);
        }
    }
}
