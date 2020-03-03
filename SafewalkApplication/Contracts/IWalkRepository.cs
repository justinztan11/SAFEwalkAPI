using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Contracts
{
    public interface IWalkRepository
    {
        Task<Walk> Get(string token, string email);

        Task<Walk> GetAll(string token);

        Task<Walk> Add(string token, Walk walk);

        Task<Walk> Update(string token, string email, Walk walk);

        Task<bool> Exists(string email);
    }
}
