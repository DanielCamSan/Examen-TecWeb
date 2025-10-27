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
            modelBuilder.Entity<Artist>(a =>
            {
                a.HasKey(a => a.Id);
                a.Property(a => a.StageName).IsRequired().HasMaxLength(100);
                a.Property(a => a.Genre).IsRequired().HasMaxLength(50);
                a.HasMany(a => a.Performances)
                  .WithOne(p => p.Artist)
                  .HasForeignKey(s => s.ArtistId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Stage>(s =>
            {
                s.HasKey(s => s.Id);
                s.Property(s => s.Name).IsRequired().HasMaxLength(100);
                s.Property(s => s.FestivalId).IsRequired();
                s.HasMany(s => s.Performances)
                  .WithOne(p => p.Stage)
                  .HasForeignKey(p => p.StageId)
                    .OnDelete(DeleteBehavior.Cascade);
            }); 

            // 1:N Festival -> Stages (FK requerida, cascade)
            modelBuilder.Entity<Festival>(f =>
            {
                f.HasKey(f => f.Id);
                f.Property(f => f.Name).IsRequired().HasMaxLength(100);
                f.Property(f => f.City).IsRequired().HasMaxLength(200);
                f.Property(f => f.StartDate).IsRequired();
                f.Property(f => f.EndDate).IsRequired();
                f.HasMany(f => f.Stages ) 
                        .WithOne(s => s.Festival)
                        .HasForeignKey(s => s.FestivalId)
                        .OnDelete(DeleteBehavior.Cascade);
            });


            // N:M con payload: Performance (clave compuesta)
            modelBuilder.Entity<Performance>(p =>
            {
                p.HasKey(p => new { p.ArtistId, p.StageId });
                p.Property(p => p.StartTime).IsRequired();
                p.Property(p => p.EndTime).IsRequired();
                p.Property(p => p.ArtistId).IsRequired();
                p.Property(p => p.StageId).IsRequired();
            }); 

            //Índice único: Stage.Name dentro de un Festival

        }
    }
}
