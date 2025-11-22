using System.Collections.Generic;

namespace Niubility.Core
{
    public interface IPagination
    {
        int PageIndex { get; }
        int PageSize { get; }

        string[] PrimaryKeys { get; }
        IEnumerable<KeyValuePair<string, OrderByDirections>> Orders { get; }
    }
}