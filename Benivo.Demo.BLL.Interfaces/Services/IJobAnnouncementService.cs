using Benivo.Demo.BLL.Interfaces.Infrastructure;
using Benivo.Demo.Models.Infrastructure;
using Benivo.Demo.Models.Inputs;
using Benivo.Demo.Models.Outputs;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Interfaces.Services
{
    public interface IJobAnnouncementService : IService
    {
        Task<CollectionPage<SearchJobAnnouncementOutput>> SearchAsync(SearchJobAnnouncementInput input, long? userId);

        Task<GetJobAnnouncementByIdOutput> GetByIdAsync(long id, long? userId);

        Task ChangeBookmarkStatusAsync(ChangeBookmarkStatusInput input);
    }
}
