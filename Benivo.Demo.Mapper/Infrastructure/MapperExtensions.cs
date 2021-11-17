using System.Collections.Generic;
using System.Linq;

namespace Benivo.Demo.Mapper.Infrastructure
{
    public static class MapperExtensions
    {
        public static TDest MapTo<TDest>(this object obj) => Mapper.Instance.Map<TDest>(obj);

        public static IEnumerable<TDest> MapTo<TDest>(this IEnumerable<object> list) 
            => list.Select(o => o.MapTo<TDest>());
    }
}
