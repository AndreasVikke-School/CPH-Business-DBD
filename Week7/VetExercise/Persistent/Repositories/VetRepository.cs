using VetExercise.DataModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VetExercise.Persistent.Repositories
{
    public class VetRepository : BaseRepository<Vet>
    {
        public VetRepository(DatabaseContext context) : base(context)
        {
        }

        #region GetByCvr
        public async Task<Vet> GetByCvrAsync(string cvr, bool includeRelated = false)
        {
            if (!includeRelated)
                return await this.context.vets.SingleOrDefaultAsync(x => x.cvr == cvr);

            var query = this.context.vets.AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.cvr == cvr);
        }
        #endregion

        #region ApplyRelations
        protected override IQueryable<Vet> ApplyRelations(IQueryable<Vet> query)
        {
            query = query
                .Include(x => x.address)
                    .ThenInclude(x => x.city);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}