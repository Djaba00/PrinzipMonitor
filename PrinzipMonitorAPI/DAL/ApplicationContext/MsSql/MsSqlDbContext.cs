using Microsoft.EntityFrameworkCore;
using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.ApplicationContext.MsSql
{
    public class MsSqlDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Flat> Flats { get; set; }

        public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.None);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            builder.Entity<Flat>(entity =>
            {
                entity.HasIndex(e => e.Url).IsUnique();
            });

            builder.Entity<User>()
                .HasMany(u => u.Flats)
                .WithMany(f => f.Users)
                .UsingEntity(j => j.ToTable("Subscribtions"));
        }
    }
}
