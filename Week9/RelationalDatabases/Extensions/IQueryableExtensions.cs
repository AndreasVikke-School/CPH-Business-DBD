using System.Linq;
using RelationalDatabases.DataModels.QueryModels;

namespace RelationalDatabases.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, BaseQueryModel queryModel) {
            // No Paging
            if(queryModel.PageSize == -1)
                return query;

            if(queryModel.Page <= 0)
                queryModel.Page = 1;

            if(queryModel.PageSize <= 0)
                queryModel.PageSize = 10;

            return query.Skip((queryModel.Page - 1) * queryModel.PageSize).Take(queryModel.PageSize);
        }
    }
}