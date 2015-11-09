using System.Data.Entity;
using AMF.Core.Model;

namespace AMF.Core.Storage
{
    public class AMFDbContext : DbContext
    {
        public DbSet<User> Animateurs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
