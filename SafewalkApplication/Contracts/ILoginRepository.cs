using SafewalkApplication.Models;
using System.Threading.Tasks;

namespace SafewalkApplication.Contracts
{
    public interface ILoginRepository
    {
        Task<User> GetUser(string email, string password);

        Task<Safewalker> GetWalker(string email, string password);

    }
}
