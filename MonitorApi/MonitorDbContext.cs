using Microsoft.EntityFrameworkCore;

namespace MonitorApi
{
    public class MonitorDbContext : DbContext
    {
        public MonitorDbContext(DbContextOptions<MonitorDbContext> options) : base(options) { }

        public DbSet<Monitor> MonitorTabela { get; set; }
    }
}
