using Benivo.Demo.BLL.Interfaces.Infrastructure;
using Benivo.Demo.Models.Outputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Interfaces.Services
{
    public interface ICityService : IService
    {
        Task<List<GetLocationOutput>> GetLocationsAsync();
    }
}
