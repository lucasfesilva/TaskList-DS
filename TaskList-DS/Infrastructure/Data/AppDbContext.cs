using Microsoft.EntityFrameworkCore;
using TaskList_DS.Domain.Entities;

namespace TaskList_DS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskEntity> taskEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}
