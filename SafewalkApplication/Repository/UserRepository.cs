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

        public async Task<User> Add(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Get(string email)
        {
            return await _context.User.SingleAsync(m => m.Email == email);
        }

        public Task<User> Update(string email, User user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Exists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Authenticated(string token)
        {
            return _context.User.AnyAsync(m => m.Token == token);
        }
    }
}
