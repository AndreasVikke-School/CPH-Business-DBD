using System.Linq;
using Microsoft.EntityFrameworkCore;
using RelationalDatabases.DataModels;
using RelationalDatabases.DataModels.QueryModels;

namespace RelationalDatabases.Persistent.Repositories
{
    public class PetRepository : BaseRepository<Pet, BaseQueryModel>
    {
        public PetRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Pet> ApplyRelations(IQueryable<Pet> query)
        {
            query = query
                .Include(x => x.caretakers)
                    .ThenInclude(x => x.address)
                        .ThenInclude(x => x.city)
                .Include(x => x.vet)
                    .ThenInclude(x => x.address)
                        .ThenInclude(x => x.city);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}