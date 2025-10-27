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
            {
                modelBuilder.Entity<Festival>(entity =>
                {
                    entity.HasKey(f => f.Id);
                    entity.Property(f => f.Name).IsRequired()
                    entity.Property(f => f.City).IsRequired()
                    entity.Property(f => f.StartDate).IsRequired();
                    entity.Property(f => f.EndDate).IsRequired();
                    entity.HasMany(f => f.Stages)
                          .WithOne(s => s.Festival)
                          .HasForeignKey(s => s.FestivalId).IsRequired();
                          .OnDelete(DeleteBehavior.Cascade);
                });
                modelBuilder.Entity<Artist>(entity =>
                {
                    entity.HasKey(a => a.Id);
                    entity.Property(a => a.StageName).IsRequired()
                    entity.Property(a => a.Genre).IsRequired()
                    entity.HasMany(a => a.Performances)
                          .WithOne(p => p.Artist)
                          .HasForeignKey(p => p.ArtistId)
                          .HasForeignKey(p=>p.StageId)
                          .HasForeignKey(p=>p.StartTime)
                });
                modelBuilder.Entity<Performance>(entity =>
                {
                    entity.Property(p =>p.ArtistId).IsRequired();
                    entity.Property(p => p.Artist);
                    entity.Property(p => p.Stage);
                    entity.Property(p => p.StageId).IsRequired();
                    entity.Property(p => p.StartTime).IsRequired();
                    entity.Property(p => p.EndTime).IsRequired();
                }
                modelBuilder.Entity<Stage>(entity =>
                {
                    entity.HasKey(s => s.Id);
                    entity.Property(s => s.Name).IsRequired();
                });

            }
    }
}
