using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using APIApp.Date;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace APIApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
          

            //DbContext Connication String 
            services.AddDbContext<AppContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            //Enable Cors 
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin",options=> options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //JSON Serializer
                        services.AddControllersWithViews()
                            .AddNewtonsoftJson(options =>
                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                            .Json.ReferenceLoopHandling.Ignore)
                            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                            = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
