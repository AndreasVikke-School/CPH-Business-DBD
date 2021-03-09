using VetExercise.DataModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetExercise.Persistent.Repositories.Pets
{
    public class CatRepository : BaseRepository<Cat>
    {
        public CatRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Cat> ApplyRelations(IQueryable<Cat> query)
        {
            query = query
                .Include(x => x.caretakers)
                .Include(x => x.vet);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}