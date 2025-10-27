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

            modelBuilder.Entity<Festival>()
                .HasMany(f => f.Stages)
                .WithOne(s => s.Festival)
                .HasForeignKey(s => s.FestivalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
                .HasKey(p => new { p.ArtistId, p.StageId, p.StartTime });

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Artist)
                .WithMany(a => a.Performances)
                .HasForeignKey(p => p.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Stage)
                .WithMany(s => s.Performances)
                .HasForeignKey(p => p.StageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stage>()
                .HasIndex(s => new { s.FestivalId, s.Name })
                .IsUnique();

        }
    }
}