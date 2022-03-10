using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence;
using Microsoft.EntityFrameworkCore;


namespace API.Extensions
{
    public static class DatabaseServices
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services,
        IConfiguration config)
        {
            // Database Connection 
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<DataContext>(); 

            return services;
        }
    }
}