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

                entity.HasOne(p => p.Artist)
                      .WithMany(a => a.Performances)
                      .HasForeignKey(p => p.ArtistId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Stage)
                      .WithMany(s => s.Performances)
                      .HasForeignKey(p => p.StageId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(p => new { p.StageId, p.StartTime, p.EndTime });
            });

            modelBuilder.Entity<Festival>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(200);
                entity.Property(f => f.City).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.StageName).IsRequired().HasMaxLength(200);
                entity.Property(a => a.Genre).IsRequired().HasMaxLength(100);
            });
        }
    }
}