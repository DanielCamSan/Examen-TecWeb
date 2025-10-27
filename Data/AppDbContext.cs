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
            modelBuilder.Entity<Stage>()
                .HasOne(s => s.Festival)
                .WithMany(f => f.Stages) 
                .HasForeignKey(s => s.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);


            // N:M con payload: Performance (clave compuesta)
            modelBuilder.Entity<Performance>()
               .HasKey(p => new { p.ArtistId, p.StageId, p.StartTime });

            //Índice único: Stage.Name dentro de un Festival
            modelBuilder.Entity<Stage>()
                .HasIndex(s => new { s.FestivalId, s.Name })
                .IsUnique();

        }
    }
}


