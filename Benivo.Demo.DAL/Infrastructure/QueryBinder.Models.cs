using Benivo.Demo.Models.Infrastructure;
using Z.EntityFramework.Plus;

namespace Benivo.Demo.DAL.Infrastructure
{
    public class QueryBinderResult<TOut>
         where TOut : BaseOutput
    {
        public QueryFutureValue<int> TotalCount { get; set; }

        public QueryFutureEnumerable<TOut> Items { get; set; }
    }
}
