using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Mapping;
using MyApp.Application.Validator;
using MyApp.Business_Core_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddTransient<IValidator<EmployeeEntity>, EmployeeEntityValidator>();
        //    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddAutoMapper(typeof(UserProfile));
            return services;
        }
    }
}
