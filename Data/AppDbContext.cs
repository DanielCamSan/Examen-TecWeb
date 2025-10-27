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
            // 1:N Festival -> Stages (FK requerida, cascade)
            modelBuilder.Entity<Festival>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(100);
                entity.Property(f => f.City).IsRequired().HasMaxLength(100);
                entity.Property(f => f.StartDate).IsRequired();
                entity.Property(f => f.EndDate).IsRequired();
                entity.HasMany(f => f.Stages)
                      .WithOne(s => s.Festival)
                      .HasForeignKey(s => s.FestivalId)
                      .OnDelete(DeleteBehavior.Cascade);

            });


         
         


            // N:M con payload: Performance (clave compuesta)
            modelBuilder.Entity<Performance>(entity =>
            {
                entity.HasKey(p => new { p.ArtistId, p.StageId});
                entity.Property(p => p.StartTime).IsRequired();
                entity.Property(p => p.EndTime).IsRequired();
                entity.HasOne(p => p.Artist)
                      .WithMany(a => a.Performances)
                      .HasForeignKey(p => p.ArtistId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(p => p.Stage)
                      .WithMany(s => s.Performances)
                      .HasForeignKey(p => p.StageId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            //Índice único: Stage.Name dentro de un Festival
            modelBuilder.Entity<Stage>()
                .HasIndex(s => new { s.FestivalId, s.Name })
                .IsUnique();

        }
    }
}
