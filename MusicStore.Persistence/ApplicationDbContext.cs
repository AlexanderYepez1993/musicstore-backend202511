using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;

namespace MusicStore.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Code first approach
            modelBuilder.Entity<Genre>().Property(x=>x.Name).HasMaxLength(50);
        }

        public DbSet<Genre> Genres { get; set; }
    }
}
