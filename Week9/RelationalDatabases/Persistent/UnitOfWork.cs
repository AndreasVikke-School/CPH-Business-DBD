using System.Threading.Tasks;

namespace RelationalDatabases.Persistent
{
    public class UnitOfWork
    {
        private readonly DatabaseContext databaseContext;

        public UnitOfWork(DatabaseContext databaseContext) {
            this.databaseContext = databaseContext;
        }

        public async Task CompletedAsync() {
            await this.databaseContext.SaveChangesAsync();
        }
    }
}