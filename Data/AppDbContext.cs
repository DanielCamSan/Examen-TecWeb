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
            modelBuilder.Entity<Festival>(static b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name);
                b.Property(x => x.City);
                b.Property(x => x.StartDate);
                b.Property(x => x.EndDate);

                b.HasMany(p => p.Stages)
                    .WithMany(o => o.Performances)
                    .HasForeignKey(o => o.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            //TODO

            // 1:N Festival -> Stages (FK requerida, cascade)
          

            // N:M con payload: Performance (clave compuesta)
          
            //Índice único: Stage.Name dentro de un Festival
            
        }
    }
}
