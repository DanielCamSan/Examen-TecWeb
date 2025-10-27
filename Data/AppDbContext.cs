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

            modelBuilder.Entity<Festival>()
                .HasMany(f => f.Stages)
                .WithOne(s => s.Festival)
                .HasForeignKey(s => s.FestivalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Performance>()
                .HasKey(p => new { p.ArtistId, p.StageId });
            modelBuilder.Entity<Stage>()
                    .HasIndex(s => new { s.FestivalId, s.Name })
                    .IsUnique();


                // 1:N Festival -> Stages (FK requerida, cascade)



                // N:M con payload: Performance (clave compuesta)

                //Índice único: Stage.Name dentro de un Festival

            }
    }
}
