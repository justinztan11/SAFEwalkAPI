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

        public async Task<Safewalker> Get(string email)
        {
            return await _context.Safewalker.SingleOrDefaultAsync(m => m.Email == email);
        }

        public async Task<Safewalker> Update(Safewalker safewalker)
        {
            _context.Entry(safewalker).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return safewalker;
        }

        public async Task<bool> Exists(string email)
        {
            return await _context.Safewalker.AnyAsync(m => m.Email == email);
        }

        public Task<bool> Authenticated(string token, string email)
        {
            return _context.Safewalker.AnyAsync(m => m.Token == token && m.Email == email);
        }
    }
}
