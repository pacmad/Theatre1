using Microsoft.EntityFrameworkCore;
using test2.Models.Performances;

namespace test2.Data
{
    public class PerformanceDbContext: DbContext
    {
        public PerformanceDbContext(DbContextOptions<PerformanceDbContext> options)
            :base(options)
        { }

        public DbSet<Performance> Performances { get; set; }
        public DbSet<PerformanceDate> PerformanceDates { get; set; }
        public DbSet<PerformanceTime> PerformanceTimes { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}
