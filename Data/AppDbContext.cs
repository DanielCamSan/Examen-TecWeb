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

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            b.Entity<Festival>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(200);
                e.Property(x => x.City).IsRequired().HasMaxLength(100);

                e.HasMany(x => x.Stages)
                 .WithOne(s => s.Festival)
                 .HasForeignKey(s => s.FestivalId)
                 .OnDelete(DeleteBehavior.Cascade);
            });


            b.Entity<Stage>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);

                e.HasIndex(x => new { x.FestivalId, x.Name }).IsUnique();

                e.HasMany(x => x.Performances)
                 .WithOne(p => p.Stage)
                 .HasForeignKey(p => p.StageId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            b.Entity<Artist>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.StageName).IsRequired().HasMaxLength(150);
                e.Property(x => x.Genre).IsRequired().HasMaxLength(100);

                e.HasMany(x => x.Performances)
                 .WithOne(p => p.Artist)
                 .HasForeignKey(p => p.ArtistId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            b.Entity<Performance>(e =>
            {
                e.HasKey(p => new { p.ArtistId, p.StageId, p.StartTime });

                e.Property(p => p.StartTime).IsRequired();
                e.Property(p => p.EndTime).IsRequired();

     
            });
        }
    }
}
