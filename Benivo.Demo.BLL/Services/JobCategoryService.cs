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
    public class JobCategoryService : Service, IJobCategoryService
    {
        public JobCategoryService(BenivoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetJobCategoriesOutput>> GetAllAsync(short? parentId)
        {
            var jobCategories = await CacheHelper.GetOrSetAsync(CacheKeys.JobCategories,
                async () => await DbContext.JobCategories.ToListAsync(),
                TimeSpan.FromHours(1));

            var filteredJobCategories = jobCategories;

            if (parentId.HasValue)
                filteredJobCategories = jobCategories.Where(jc => jc.ParentId == parentId).ToList();
            else
                filteredJobCategories = jobCategories.Where(jc => !jc.ParentId.HasValue).ToList();

            return filteredJobCategories.Select(jc => new GetJobCategoriesOutput
            {
                Id = jc.Id,
                ParentId = jc.ParentId,
                HasChild = jobCategories.Any(sub => sub.ParentId == jc.Id)
            }).ToList();
        }
    }
}
