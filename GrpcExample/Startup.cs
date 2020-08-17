using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using GrpcExample.DataAccess;
using GrpcExample.Models;
using GrpcExample.GrpcControllers;

namespace GrpcExample
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
            services.Configure<SchoolDatabaseSettings>(
                Configuration.GetSection(nameof(SchoolDatabaseSettings)));

            services.AddSingleton<ISchoolDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<SchoolDatabaseSettings>>().Value);

            services.AddSingleton<StudentDataAccess>();
            services.AddGrpc();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<StudentGrpcController>();
            });
        }
    }
}
