using SafewalkApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafewalkApplication.Contracts
{
    public interface IWalkRepository
    {
        Task<Walk> Get(string email);

        IEnumerable<Walk> GetAll();

        Task<Walk> Add(Walk walk);

        Task<Walk> Update(Walk walk);

        Task<Walk> Delete(string id);

        Task<bool> Exists(string email);
    }
}
