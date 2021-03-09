using System.Linq;
using Microsoft.EntityFrameworkCore;
using RelationalDatabases.DataModels;
using RelationalDatabases.DataModels.QueryModels;

namespace RelationalDatabases.Persistent.Repositories
{
    public class AddressRepository : BaseRepository<Address, BaseQueryModel>
    {
        public AddressRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Address> ApplyRelations(IQueryable<Address> query)
        {
            query = query
                .Include(x => x.city);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}