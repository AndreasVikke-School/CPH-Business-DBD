using VetExercise.DataModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VetExercise.Persistent.Repositories
{
    public class CaretakerRepository : BaseRepository<Caretaker>
    {
        public CaretakerRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Caretaker> ApplyRelations(IQueryable<Caretaker> query)
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