using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookstore2.Models;
using bookstore2.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace bookstore2
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration Configuration)
        {
           this.configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMvc(option => option.EnableEndpointRouting=false);
            services.AddScoped<IBookstoreRepository<Author>, AuthorDbRepository>();
            services.AddScoped<IBookstoreRepository<Book>, BookDbRepository>();

            
            services.AddDbContext<BookStoreDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Sqlcon")));

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();


            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
			endpoints.MapControllerRoute("default","{controller=Book}/{action=Index}");
            });
        }
    }
}
