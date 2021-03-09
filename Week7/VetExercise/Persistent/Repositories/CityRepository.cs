using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetExercise.DataModels;

namespace VetExercise.Persistent.Repositories
{
    public class CityRepository : BaseRepository<City>
    {
        public CityRepository(DatabaseContext context) : base(context)
        {
        }

        #region GetByZip
        public async Task<City> GetByZipAsync(int zip, bool includeRelated = false)
        {
            if (!includeRelated)
                return await this.context.cities.SingleOrDefaultAsync(x => x.zip == zip);

            var query = this.context.cities.AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.zip == zip);
        }
        #endregion
    }
}