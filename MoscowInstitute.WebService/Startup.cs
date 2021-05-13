using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using MoscowPayphones.ApplicationServices.GetPayphonesListUseCase;
using MoscowPayphones.ApplicationServices.Ports.Gateways.Database;
using MoscowPayphones.InfrastructureServices.Gateways.Database;
using Microsoft.EntityFrameworkCore;
using MoscowPayphones.ApplicationServices.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MoscowPayphones.WebService.InfrastructureServices.Gateways;
using MoscowPayphones.WebService.Scheduler;

namespace MoscowPayphones.WebService 
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
            services.AddDbContext<PayphonesContext>(opts =>
                opts.UseSqlite($"Filename={System.IO.Path.Combine(System.Environment.CurrentDirectory, "MoscowPayphones.db")}")
            );
            services.AddHostedService<ScheduleTask>();
            services.AddScoped<IPayphonesDatabaseGateway, PayphonesEFSqliteGateway>();

            services.AddScoped<DbPayphonesRepository>();
            services.AddScoped<IReadOnlyPayphonesRepository>(x => x.GetRequiredService<DbPayphonesRepository>());
            services.AddScoped<IPayphonesRepository>(x => x.GetRequiredService<DbPayphonesRepository>());

            services.AddScoped<IGetPayphonesListUseCase, GetPayphonesListUseCase>();

            services.AddControllers();
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
                endpoints.MapControllers();
            });
        }
    }
}
