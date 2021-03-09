using System.Collections.Generic;

namespace RelationalDatabases.DataModels.QueryModels
{
    public class BaseQueryResult<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public int Count { get; set; }
    }
}