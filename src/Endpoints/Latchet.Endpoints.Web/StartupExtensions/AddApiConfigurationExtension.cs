using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latchet.Utilities.Configurations;
using Latchet.Endpoints.Web.Filters;
using Latchet.Endpoints.Web.Middlewares;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace Latchet.Endpoints.Web.StartupExtensions
{
    public static class AddApiConfigurationExtention
    {
        public static IServiceCollection AddLatchetApiServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var latchetConfiguration = new LatchetConfiguration();
            configuration.GetSection(nameof(LatchetConfiguration)).Bind(latchetConfiguration);
            services.AddSingleton(latchetConfiguration);
            services.AddScoped<ValidateModelStateAttribute>();
            services.AddControllers(options =>
            {
                options.Filters.AddService<ValidateModelStateAttribute>();
                options.Filters.Add(typeof(TrackActionPerformanceFilter));
            }).AddFluentValidation();

            services.AddLatchetDependencies(latchetConfiguration.AssmblyNameForLoad.Split(','));
            if (latchetConfiguration.Swagger.Enabled)
                AddSwagger(services);
            return services;
        }

        private static void AddSwagger(IServiceCollection services)
        {
            var latchetConfigurations = services.BuildServiceProvider().GetService<LatchetConfiguration>();
            if (latchetConfigurations.Swagger != null && latchetConfigurations.Swagger.SwaggerDoc != null)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(latchetConfigurations.Swagger.SwaggerDoc.Name, new OpenApiInfo { Title = latchetConfigurations.Swagger.SwaggerDoc.Title, Version = latchetConfigurations.Swagger.SwaggerDoc.Version });
                });
            }
        }
        public static void UseLatchetApiConfigure(this IApplicationBuilder app, LatchetConfiguration configuration, IWebHostEnvironment env)
        {
            app.UseApiExceptionHandler(options =>
            {
                options.AddResponseDetails = (context, ex, error) =>
                {
                    if (ex.GetType().Name == typeof(SqlException).Name)
                    {
                        error.Detail = "Exception was a database exception!";
                    }
                };
                options.DetermineLogLevel = ex =>
                {
                    if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                        ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return LogLevel.Critical;
                    }
                    return LogLevel.Error;
                };
            });

            app.UseStatusCodePages();
            if (configuration.Swagger != null && configuration.Swagger.SwaggerDoc != null && configuration.Swagger.Enabled)
            {

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(configuration.Swagger.SwaggerDoc.URL, configuration.Swagger.SwaggerDoc.Title);
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }




    }
}
