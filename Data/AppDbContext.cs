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

            //TODO

            // 1:N Festival -> Stages (FK requerida, cascade)
            modelBuilder.Entity<Festival>()
                .HasMany(f => f.Stages)
                .WithOne(s => s.Festival)
                .HasForeignKey(s => s.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);

            // N:M con payload: Performance (clave compuesta)
            modelBuilder.Entity<Performance>()
                .HasKey(p => new { p.ArtistId, p.StageId, p.StartTime});
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Artist)
                .WithMany(a => a.Performances)
                .HasForeignKey(p => p.ArtistId);
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Stage)
                .WithMany(s => s.Performances)
                .HasForeignKey(p => p.StageId);

            //Índice único: Stage.Name dentro de un Festival
            
        }
    }
}
