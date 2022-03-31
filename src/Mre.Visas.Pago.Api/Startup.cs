using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mre.Sb.Auditar;
using Mre.Visas.Pago.Api.Extensions;
using Mre.Visas.Pago.Application;
using Mre.Visas.Pago.Infrastructure;
using Mre.Visas.Pago.Infrastructure.Persistence.Contexts;
using Newtonsoft.Json;
using System;

namespace Mre.Visas.Pago.Api
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
            services.AddInfrastructureLayer(Configuration);
            services.AddApplicationLayer();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //ADD CROSS
            services.AddCors();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c =>
                  {
                      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mre.Visas.Pago.Api", Version = "v1" });
                  });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("Redis");
                options.InstanceName = "Mre.Visas.Pago:";
            });
            services.AgregarAuditoria(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mre.Visas.Pago.Api v1"));
            }

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwaggerExtension("Mre.Visas.Pago.Api");
            app.UseApiExceptionMiddleware();

            //ADD CROSS
            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UsarAuditoria<ApplicationDbContext>();
        }
    }
}