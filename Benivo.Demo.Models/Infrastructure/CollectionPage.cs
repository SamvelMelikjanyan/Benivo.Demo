using System.Collections.Generic;

namespace Benivo.Demo.Models.Infrastructure
{
    public class CollectionPage<T> where T : BaseOutput
    {
        public CollectionPage(IList<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public IList<T> Items { get; private set; }

        public int TotalCount { get; private set; }
    }
}
