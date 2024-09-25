using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Business_Core_Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDependencyInjection(this IServiceCollection services)
        {
            return services;
        }
    }
}
