using Microsoft.EntityFrameworkCore;
using SafewalkApplication.Contracts;
using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class SafewalkerRepository : ISafewalkerRepository
    {
        private readonly SafewalkDatabaseContext _context;

        public SafewalkerRepository(SafewalkDatabaseContext context)
        {
            _context = context;
        }

        public Task<Safewalker> Get(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(string email, Safewalker safewalker)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Authenticated(string token)
        {
            return _context.Safewalker.AnyAsync(m => m.Token == token);
        }
    }
}
