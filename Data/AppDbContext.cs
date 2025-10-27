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
            modelBuilder.Entity<Festival>(b =>
            {
                b.HasMany(festival => festival.Stages)
                  .WithOne(stage => stage.Festival)
                  .HasForeignKey(stage => stage.FestivalId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            // N:M con payload: Performance (clave compuesta)
          
            //Índice único: Stage.Name dentro de un Festival
            
        }
    }
}
