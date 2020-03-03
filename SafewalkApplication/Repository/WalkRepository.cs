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

        public Task<Walk> Add(string token, Walk walk)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Walk> Get(string token, string email)
        {
            throw new NotImplementedException();
        }

        public Task<Walk> GetAll(string token)
        {
            throw new NotImplementedException();
        }

        public Task<Walk> Update(string token, string email, Walk walk)
        {
            throw new NotImplementedException();
        }
    }
}
