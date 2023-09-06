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
    }
}
