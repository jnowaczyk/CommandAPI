using CommandAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using AutoMapper;
using System;
using Newtonsoft.Json.Serialization;

namespace CommandAPI
{
    public class Startup
    {
        // IConfiguration daje dostep do "configuration API" 
        // to pozwala uzyskac dostęp do konfiguracji trzymanych m.in. w appsettings.json
        // 

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }




        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ICommandAPIRepo,SqlCommandApiRepo>();

            var connBuilder = new NpgsqlConnectionStringBuilder();
            connBuilder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
            connBuilder.Username = Configuration["UserID"];
            connBuilder.Password = Configuration["Password"];
            
            services.AddDbContext<CommandContext>( options => options.UseNpgsql(connBuilder.ConnectionString));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers().AddNewtonsoftJson( setup => 
            {
                setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
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
