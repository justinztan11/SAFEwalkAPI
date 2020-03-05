using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Contracts
{
    public interface IWalkRepository
    {
        Task<Walk> Get(string email);

        Task<Walk> GetAll();

        Task<Walk> Add(Walk walk);

        Task<Walk> Update(string email, Walk walk);

        Task<bool> Exists(string email);
    }
}
