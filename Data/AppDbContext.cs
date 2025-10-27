using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Festival> Festivals => Set<Festival>();
        public DbSet<Stage> Stages => Set<Stage>();
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Performance> Performances => Set<Performance>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Festival>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(100);
                entity.Property(f => f.City).IsRequired().HasMaxLength(200);
                entity.Property(f => f.StartDate).IsRequired();
                entity.Property(f => f.EndDate).IsRequired();
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(s => s.Festival)
                      .WithMany(f => f.Stages)
                      .HasForeignKey(s => s.FestivalId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(s => new { s.FestivalId, s.Name }).IsUnique();
            });

            // N:M con payload: Performance (clave compuesta)

            //Índice único: Stage.Name dentro de un Festival

        }
    }
}
