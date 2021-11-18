using Benivo.Demo.BLL.Interfaces.Infrastructure;
using Benivo.Demo.Models.Outputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Interfaces.Services
{
    public interface IJobCategoryService : IService
    {
        Task<List<GetJobCategoriesOutput>> GetAllAsync(short? parentId);
    }
}
