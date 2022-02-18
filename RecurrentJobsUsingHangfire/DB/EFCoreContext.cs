using Microsoft.EntityFrameworkCore;

namespace RecurrentJobsUsingHangfire
{
    public class EFCoreContext : DbContext
    {
        public DbSet<Good> Goods { get; set; }

        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
