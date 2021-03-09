using VetExercise.DataModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetExercise.Persistent.Repositories.Pets
{
    public class PetRepository : BaseRepository<Pet>
    {
        public PetRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Pet> ApplyRelations(IQueryable<Pet> query)
        {
            query = query
                .Include(x => x.caretakers)
                .Include(x => x.vet);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}