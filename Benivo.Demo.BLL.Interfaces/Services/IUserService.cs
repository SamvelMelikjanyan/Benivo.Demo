using Benivo.Demo.BLL.Interfaces.Infrastructure;
using Benivo.Demo.Models.Inputs;
using Benivo.Demo.Models.Outputs;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Interfaces.Services
{
    public interface IUserService : IService
    {
        Task<RegisterOutput> RegisterAsync(RegisterInput registerInput);
    }
}
