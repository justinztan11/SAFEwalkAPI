using Microsoft.EntityFrameworkCore;
using SafewalkApplication.Contracts;
using SafewalkApplication.Models;
using System;
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

        public async Task<bool> Exists(string id)
        {
            return await _context.Walk.AnyAsync(m => m.Id == id);
        }

        public async Task<Walk> Get(string id)
        {
            return await _context.Walk.SingleAsync(m => m.Id == id);
        }

        public IEnumerable<Walk> GetAll()
        {
            return _context.Walk;
        }

        public Task<Walk> Update(string id, Walk walk)
        {
            throw new NotImplementedException();
        }
    }
}
