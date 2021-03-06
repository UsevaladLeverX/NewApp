using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using NewApp.Domain.Interfaces;
using NewApp.Infrastructure.Data;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.Web;
using System.Fabric.Query;
using AutoMapper;

namespace NewApp
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
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            var connection = @"Server=LXIBY1166\SQLEXPRESS;Database=LeverApp;Trusted_Connection=True;";
            services.AddDbContext<OnionAppContext>(options => options.UseSqlServer(connection));
            services.AddMvc();
            //services.AddScoped<IService>(provider => {
            //    var dependency = provider.GetRequiredService<IDependency>();
            //    // You can select the constructor you want here.
            //    return new Service(dependency);
            //});
            //services.AddScoped<IRepository<>, MenteeRepository>();
            //services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
