using Benivo.Demo.BLL.Infrastructure;
using Benivo.Demo.BLL.Interfaces.Services;
using Benivo.Demo.Cache;
using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Models.Outputs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Services
{
    public class CityService : Service, ICityService
    {
        public CityService(BenivoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetLocationOutput>> GetLocationsAsync()
            => await CacheHelper.GetOrSetAsync(CacheKeys.Locations, async () =>
            await DbContext.Cities.Select(c => new GetLocationOutput
            {
                Id = c.Id,
                City = c.Name,
                Country = c.Country.Name
            }).ToListAsync(), TimeSpan.FromHours(1));
    }
}
