using System;
using Microsoft.Extensions.DependencyInjection;
using Benivo.Demo.BLL.Interfaces.Services;
using Benivo.Demo.ThirdPartyServices.Services;

namespace Benivo.Demo.Api.Infrastructure
{
    public class ServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public BenivoIdentityService BenivoIdentityService => _serviceProvider.GetService<BenivoIdentityService>();
    
        public IUserService UserService => _serviceProvider.GetService<IUserService>();

        public IJobAnnouncementService JobAnnouncementService => _serviceProvider.GetService<IJobAnnouncementService>();
    
        public IJobCategoryService JobCategoryService => _serviceProvider.GetService<IJobCategoryService>();
    }
}
