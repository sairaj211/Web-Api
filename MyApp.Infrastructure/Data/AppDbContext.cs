using Microsoft.EntityFrameworkCore;
using MyApp.Business_Core_Domain.Entities;

namespace MyApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
    }
}
