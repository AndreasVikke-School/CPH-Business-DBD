using System.Linq;
using Microsoft.EntityFrameworkCore;
using VetExercise.DataModels;

namespace VetExercise.Persistent.Repositories
{
    public class AddressRepository : BaseRepository<Address>
    {
        public AddressRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Address> ApplyRelations(IQueryable<Address> query)
        {
            query = query
                .Include(x => x.city)
                .Include(x => x.vets);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}