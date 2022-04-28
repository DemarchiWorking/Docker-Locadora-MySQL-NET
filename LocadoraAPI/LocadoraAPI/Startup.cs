using Application.Service;
using Application.Service.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IO;

namespace LocadoraAPI
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

            string stringDeConexao = Configuration.GetConnectionString("conexaoMySQL");

            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocadoraAPI", Version = "v1" });
            });

            services.AddSingleton((ILogger)new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(Path.Combine("/var/log/ms_locadora", "ms_locadora.log"), rollingInterval: RollingInterval.Day)
            .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
            .CreateLogger());


            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IFilmeService, FilmeService>();
            services.AddScoped<ILocacaoService, LocacaoService>();
            services.AddScoped<IFuncionalidadesService, FuncionalidadesService>();


            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<ILocacaoRepository, LocacaoRepository>();
            services.AddScoped<IFuncionalidadesRepository, FuncionalidadesRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocadoraAPI v1"));
            }
            app.UseHttpsRedirection();

            app.UseCors(builder =>
             builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
