using System.Collections.Generic;

namespace PhotoSearch.Api.Infrastructure
{
    public interface IPage<T>
    {
        /// <summary>
        /// Gets the pages count.
        /// </summary>
        /// <value>
        /// The pages.
        /// </value>
        int Pages { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        IReadOnlyList<T> Data { get; }
    }
}
