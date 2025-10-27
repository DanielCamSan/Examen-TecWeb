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
            // 1:N Festival -> Stages (FK requerida, cascade)
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Festival>(b =>
            {
                b.HasKey(f => f.Id);
                b.Property(f => f.Name);
                b.Property(f => f.StartDate);
                b.Property(f => f.EndDate);
                b.HasMany(f => f.stages)
                .WithOne(f => f.Festival)
                .HasForeignKey(f => f.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);

            }
            );
            // N:M con payload: Performance (clave compuesta)
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Performance>(b =>
            {
                b.HasKey(f => new { f.ArtistId, f.StageId });
                b.Property(f => f.StartTime);
                b.Property(f => f.EndTime);
                b.HasOne(f => f.Artist)
                .WithMany(f => f.Performances)
                .HasForeignKey(f => f.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(f => f.Stage)
               .WithMany(f => f.Performances)
               .HasForeignKey(f => f.StageId)
               .OnDelete(DeleteBehavior.Cascade);


            });
            //TODO


            //Índice único: Stage.Name dentro de un Festival
            modelBuilder.Entity<Stage>(b =>
            {
                b.HasIndex(f => new { f.FestivalId, f.Name })
                .IsUnique();
            });
        }
    }
}
