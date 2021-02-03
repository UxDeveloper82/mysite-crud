using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mysite.Data;
using mysite.Data.FileManager;
using mysite.Data.Repository;
using mysite.Extensions;

namespace mysite
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationService(_config);
            services.AddIdentityServices(_config);
            services.AddTransient<Seed>();
            services.AddMvc(options => 
            {
                options.CacheProfiles.Add("Monthly", new CacheProfile { Duration= 60*60* 24 *7 *4});   
            }).AddJsonOptions(opt => {
                opt.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //seeder.SeedMembers();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            // app.Run(async (context) =>
            // {
            //  await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
