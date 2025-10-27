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
            /* a.HasKey(x => x.id);
                  a.Property(x => x.id).ValueGeneratedNever();
                  a.Property(x => x.name).IsRequired().HasMaxLength(200);
                  a.HasMany(a => a.Books)
                       .WithOne(b => b.Author)
                       .HasForeignKey(b => b.AuthorId)
                       .OnDelete(DeleteBehavior.Restrict);
            
             
              modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).IsRequired().HasMaxLength(200);
                b.Property(x => x.Year).IsRequired();
                b.HasIndex(x => x.AuthorId);
            });*/


            // N:M con payload: Performance (clave compuesta)

            //Índice único: Stage.Name dentro de un Festival

        }
    }
}
