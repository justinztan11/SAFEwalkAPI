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

        public Task<Walk> Add(Walk walk)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Walk> Get(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Walk> GetAll()
        {
            return _context.Walk;
        }

        public Task<Walk> Update(string email, Walk walk)
        {
            throw new NotImplementedException();
        }
    }
}
