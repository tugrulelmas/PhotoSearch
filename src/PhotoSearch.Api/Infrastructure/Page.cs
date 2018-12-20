using System.Collections.Generic;

namespace PhotoSearch.Api.Infrastructure
{
    public class Page<T> : IPage<T>
    {
        public int Pages { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
