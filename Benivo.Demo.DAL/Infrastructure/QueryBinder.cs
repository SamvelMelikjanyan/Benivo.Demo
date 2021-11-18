using Benivo.Demo.Models.Infrastructure;

namespace Benivo.Demo.DAL.Infrastructure
{
    public abstract class QueryBinder<TIn, TOut>
         where TIn : BasePaginationInput
         where TOut : BaseOutput
    {
        protected readonly BenivoDbContext DbContext;

        public QueryBinder(BenivoDbContext benivoDbContext) => DbContext = benivoDbContext;

        public abstract QueryBinderResult<TOut> Bind(TIn input);
    }
}
