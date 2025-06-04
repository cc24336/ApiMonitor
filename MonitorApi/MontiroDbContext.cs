using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace MonitorApi
{
    public class MonitorDbContext : DbContext
    {
        public MonitorDbContext(DbContextOptions<MonitorDbContext> options) : base(options)
        { }

        public DbSet<Horario> HorarioTabela { get; set; }
        public DbSet<Monitor> MonitorTabela { get; set; }
    }
}