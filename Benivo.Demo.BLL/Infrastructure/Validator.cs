using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Infrastructure;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Infrastructure
{
    internal abstract class Validator<TEntity> where TEntity : BaseEntity
    {
        protected readonly BenivoDbContext DbContext;

        public Validator(BenivoDbContext dbContext) => DbContext = dbContext;

        public abstract Task ValidateAsync(TEntity entity);
    }
}
