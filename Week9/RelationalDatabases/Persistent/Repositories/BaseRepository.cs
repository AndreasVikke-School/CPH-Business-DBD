using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelationalDatabases.DataModels;
using RelationalDatabases.DataModels.QueryModels;
using RelationalDatabases.Extensions;

namespace RelationalDatabases.Persistent.Repositories
{
    public class BaseRepository<T, Q>
        where T : BaseModel
        where Q : BaseQueryModel
    {
        protected readonly DatabaseContext databaseContext;

        public BaseRepository(DatabaseContext databaseContext) {
            this.databaseContext = databaseContext;
        }

        public virtual async Task<T> GetAsync(long entityId, bool includeRelated = false) {
            if(!includeRelated)
                return await this.databaseContext.Set<T>().FindAsync(entityId);
                // SELECT * FROM City WHERE Id = entityid

            var query = this.databaseContext.Set<T>().AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.Id == entityId);
        }

        public virtual async Task<BaseQueryResult<T>> GetAllAsync(Q  queryModel, bool includeRelated = false) {
            var result = new BaseQueryResult<T>();
            var query = this.databaseContext.Set<T>().AsQueryable();

            // Filtering
            query = this.ApplyFiltering(query, queryModel);

            // Result Count
            result.Count = await query.CountAsync();

            // Paging
            query = query.ApplyPaging(queryModel);

            // Relations
            if (includeRelated)
                query = this.ApplyRelations(query);

            result.Entities = await query.ToListAsync();

            return result;
        }


        #region Filters / Relations
        protected virtual IQueryable<T> ApplyFiltering(IQueryable<T> query, Q queryModel) {
            return query;
        }

        protected virtual IQueryable<T> ApplyRelations(IQueryable<T> query) {
            return query;
        }
        #endregion

        public void Add(T entity) {
            this.databaseContext.Set<T>().Add(entity);
        }

        public void Update(T entity) {
            this.databaseContext.Set<T>().Update(entity);
        }

        public void Remove(T entity) {
            this.databaseContext.Set<T>().Remove(entity);
        }
    }
}