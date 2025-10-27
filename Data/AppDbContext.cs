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
          
            //�ndice �nico: Stage.Name dentro de un Festival
            
        }
    }
}
