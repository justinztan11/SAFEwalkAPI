using SafewalkApplication.Models;
using System.Threading.Tasks;

namespace SafewalkApplication.Contracts
{
    public interface ILoginRepository
    {
        Task<string> GetUser(string email, string password);

        Task<string> GetWalker(string email, string password);

    }
}
