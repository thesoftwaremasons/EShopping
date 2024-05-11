using Catalog.Application.Handler;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repository;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;


namespace Catalog.Api
{
    public class Startup
    {
        public IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            services.AddHealthChecks().AddMongoDb(configuration["DatabaseSettings:ConnectionString"],"Catalog Mongo Db Health Check",HealthStatus.Degraded);
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });

            services.AddAutoMapper(typeof(Startup));
          //  services.AddMediatR(typeof(CreateProductHandler).GetTypeInfo().Assembly);
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly));
           // services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, ProductRepository>();
            services.AddScoped<ITypesRepository, ProductRepository>();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","Catalog V1"));
            }
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                });
            });
        }
    }
}
