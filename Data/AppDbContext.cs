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
            modelBuilder.Entity<Festival>(f =>
            {
                f.HasKey(f => f.Id);
                f.Property(f => f.Name).IsRequired().HasMaxLength(100);
                f.HasMany(f => f.Stages).WithOne(s => s.Festival).HasForeignKey(s => s.FestivalId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Performance>(p =>
            {
                p.HasKey(p => new { p.ArtistId, p.StageId });

            });

            modelBuilder.Entity<Stage>(s => {                 
                s.HasKey(s => s.Id);
                s.Property(s => s.Name).IsRequired().HasMaxLength(100);
                s.HasIndex(s => new { s.FestivalId, s.Name }).IsUnique();
            });

            modelBuilder.Entity<Artist>(a =>
            {
                a.HasKey(a => a.Id);
                a.Property(a => a.StageName).IsRequired().HasMaxLength(100);
                a.Property(a => a.Genre).IsRequired().HasMaxLength(50);
            });
            //TODO

            // 1:N Festival -> Stages (FK requerida, cascade)


            // N:M con payload: Performance (clave compuesta)

            //Índice único: Stage.Name dentro de un Festival

        }
    }
}
