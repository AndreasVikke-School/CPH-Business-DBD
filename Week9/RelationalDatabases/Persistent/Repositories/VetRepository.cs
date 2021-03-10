using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelationalDatabases.DataModels;
using RelationalDatabases.DataModels.QueryModels;

namespace RelationalDatabases.Persistent.Repositories
{
    public class VetRepository : BaseRepository<Vet, BaseQueryModel>
    {
        public VetRepository(DatabaseContext context) : base(context)
        {
        }

        #region GetByCVR
        public async Task<Vet> GetByCVRAsync(string cvr, bool includeRelated = false) {
            if(!includeRelated)
                return await this.databaseContext.vets.SingleOrDefaultAsync(x => x.cvr == cvr);

            var query = this.databaseContext.vets.AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.cvr == cvr);
        }
        #endregion

        #region ApplyRelations
        protected override IQueryable<Vet> ApplyRelations(IQueryable<Vet> query)
        {
            query = query
                .Include(x => x.address)
                    .ThenInclude(x => x.city)
                .Include(x => x.pets);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}