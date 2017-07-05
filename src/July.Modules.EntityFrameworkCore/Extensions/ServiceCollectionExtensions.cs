using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Modules.EntityFrameworkCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJulyDbContext<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> builderAction)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(builderAction, ServiceLifetime.Scoped);
            
        }
    }
}
