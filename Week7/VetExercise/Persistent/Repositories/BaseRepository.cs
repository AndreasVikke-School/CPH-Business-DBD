using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetExercise.DataModels;
using VetExercise.DataModels.QueryModels;

namespace VetExercise.Persistent.Repositories
{
    public abstract class BaseRepository<T>
           where T : BaseModel
    {
        protected readonly DatabaseContext context;
        
        public BaseRepository(DatabaseContext context) {
            this.context = context;
        }

        public virtual async Task<T> GetAsync(long entityId, bool includeRelated = false) {
            if (!includeRelated)
                return await this.context.Set<T>().FindAsync(entityId);

            var query = this.context.Set<T>().AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.Id == entityId);
        }

        public virtual async Task<BaseQueryResult<T>> GetAllAsync(bool includeRelated = false, bool disableTracking = true)
        {
            var result = new BaseQueryResult<T>();
            var query = this.context.Set<T>().AsQueryable();

            // Count
            result.Count = await query.CountAsync();

            // Relations
            if (includeRelated)
                query = this.ApplyRelations(query);

            // Querying
            if (disableTracking)
                result.Entities = await query.AsNoTracking().ToListAsync();
            else
                result.Entities = await query.ToListAsync();

            return result;
        }

        #region Filters / Relations
        protected virtual IQueryable<T> ApplyRelations(IQueryable<T> query)
        {
            return query;
        }
        #endregion

        public void Add(T entity)
        {
            this.context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }
    }
}