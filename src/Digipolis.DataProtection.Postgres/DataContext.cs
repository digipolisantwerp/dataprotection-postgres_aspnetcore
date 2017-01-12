using Microsoft.EntityFrameworkCore;

namespace Digipolis.DataProtection.Postgres
{
    internal class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> Options)
            : base(Options)
        {

        }

        public DbSet<KeyValuesCollection> KeyCollections { get; set; }
    }
}
