
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Business_Core_Domain.Interfaces;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencyInjection(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=DESKTOP-UEPM6FT\\SQLEXPRESS;Database=TestAPIDb;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
