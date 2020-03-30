using Microsoft.EntityFrameworkCore;
using SafewalkApplication.Contracts;
using SafewalkApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly SafewalkDatabaseContext _context;

        public WalkRepository(SafewalkDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Walk> Add(Walk walk)
        {
            await _context.Walk.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> Get(string id)
        {
            return await _context.Walk.SingleOrDefaultAsync(m => m.Id == id);
        }

        public IEnumerable<Walk> GetAll()
        {
            return _context.Walk.Where(m => m.Status == 0);
        }

        public async Task<Walk> Update(Walk walk)
        {
            _context.Walk.Update(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> Delete(string id)
        {
            var walk = await _context.Walk.SingleOrDefaultAsync(m => m.Id == id);
            _context.Walk.Remove(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<bool> Exists(string email)
        {
            return await _context.Walk.AnyAsync(m => m.UserEmail == email 
                && (m.Status == 0 || m.Status == 1));
        }
    }
}
