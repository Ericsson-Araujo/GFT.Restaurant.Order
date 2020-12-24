using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFT.Restaurant.Order.BLL;
using GFT.Restaurant.Order.DAL;
using GFT.Restaurant.Order.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GFT.Restaurant.Order.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurations (Controllers, Database, Injection e CORS)

            services.AddDbContext<Context>(
                x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
                        
            services.AddMvc(a => a.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<IDishBLL, DishBLL>();
            services.AddScoped<IDishDAL, DishDAL>();
                        
            services.AddCors();

            //services.AddControllers();   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseCors(configurePolicy: x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseMvc();
        }
    }
}
