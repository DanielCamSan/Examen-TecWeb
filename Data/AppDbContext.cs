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

            // 1:N Festival -> Stages (FK requerida, cascade) : HECHO
            modelBuilder.Entity<Stage>()
                .HasOne(s => s.Festival) // un festival
                .WithMany(f => f.Stages) // muchos escenarios
                .HasForeignKey(s => s.FestivalId) //clave foranea
                .IsRequired() // la clave foranea es requerida
                .OnDelete(DeleteBehavior.Cascade);

            // N:M con payload: Performance (clave compuesta)
            modelBuilder.Entity<Performance>()
                .HasKey(p => new {p.ArtistId, p.StageId, p.StartTime});
            modelBuilder.Entity<Performance>()
                .HasOne(a => a.Artist);
            //.///VOLVEEEEEEEEEEEEEEEEEEEEEER
            modelBuilder.Entity<Festival>();
                
            //Índice único: Stage.Name dentro de un Festival

        }
    }
}
