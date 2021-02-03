using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mysite.Data;
using mysite.Data.FileManager;
using mysite.Data.Repository;
using mysite.Interfaces;
using mysite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IPortRepository, PortRepository>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<ITechRepository, TechRepository>();
            services.AddAutoMapper(typeof(TechRepository).Assembly);
            services.AddDbContext<DataContext>(x => 
            x.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            return services;
        }
        
    }
}
