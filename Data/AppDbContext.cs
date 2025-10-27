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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasOne(s => s.Festival)
                      .WithMany(f => f.Stages)
                      .HasForeignKey(s => s.FestivalId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(); 

                entity.HasIndex(s => new { s.FestivalId, s.Name })
                      .IsUnique();
            });

            modelBuilder.Entity<Performance>(entity =>
            {
                entity.HasKey(p => new { p.ArtistId, p.StageId, p.StartTime });

                
            });

            
        }
    }
}