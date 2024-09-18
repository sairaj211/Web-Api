using Microsoft.EntityFrameworkCore;
using MyApp.Business_Core_Domain.Entities;

namespace MyApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<EmployeeEntity> Employees { get; set; }
    }

}
