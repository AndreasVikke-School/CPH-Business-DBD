using VetExercise.DataModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VetExercise.Persistent.Repositories.Pets
{
    public class DogRepository : BaseRepository<Dog>
    {
        public DogRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Dog> ApplyRelations(IQueryable<Dog> query)
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