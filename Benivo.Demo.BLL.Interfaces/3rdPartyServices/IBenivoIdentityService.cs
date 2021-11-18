using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Interfaces._3rdPartyServices
{
    public interface IBenivoIdentityService
    {
        Task<int> RegisterUser(string username, string email, string password, long id);
    }
}
