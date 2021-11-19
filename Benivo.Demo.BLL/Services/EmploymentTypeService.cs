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
    public class EmploymentTypeService : Service, IEmploymentTypeService
    {
        public EmploymentTypeService(BenivoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetEmploymentTypeOutput>> GetAllAsync()
        {
            var employmentTypes = await CacheHelper.GetOrSetAsync(CacheKeys.EmploymentTypes,
                async () => await DbContext.EmploymentTypes.AsNoTracking().ToListAsync(),
                TimeSpan.FromHours(1));

            return employmentTypes.Select(et => new GetEmploymentTypeOutput
            {
                Id = et.Id,
                Name = et.Name
            }).ToList();
        }
    }
}
