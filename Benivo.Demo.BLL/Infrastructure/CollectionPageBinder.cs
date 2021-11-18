using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Models.Infrastructure;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Infrastructure
{
    internal class CollectionPageBinder<TIn, TOut>
         where TIn : BasePaginationInput
         where TOut : BaseOutput
    {
        private readonly QueryBinder<TIn, TOut> _queryBinder;

        public CollectionPageBinder(QueryBinder<TIn, TOut> queryBinder) => _queryBinder = queryBinder;

        public async Task<CollectionPage<TOut>> BindAsync(TIn input)
        {
            var query = _queryBinder.Bind(input);

            return new(await query.Items.ToListAsync(), await query.TotalCount.ValueAsync());
        }
    }
}
