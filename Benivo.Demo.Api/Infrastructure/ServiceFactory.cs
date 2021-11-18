using Benivo.Demo.BLL.Interfaces._3rdPartyServices;
using System;
using Microsoft.Extensions.DependencyInjection;
using Benivo.Demo.BLL.Interfaces.Services;

namespace Benivo.Demo.Api.Infrastructure
{
    public class ServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public IBenivoIdentityService BenivoIdentityService => _serviceProvider.GetService<IBenivoIdentityService>();
    
        public IUserService UserService => _serviceProvider.GetService<IUserService>();

        public IJobAnnouncementService JobAnnouncementService => _serviceProvider.GetService<IJobAnnouncementService>();
    
        public IJobCategoryService JobCategoryService => _serviceProvider.GetService<IJobCategoryService>();
    }
}
