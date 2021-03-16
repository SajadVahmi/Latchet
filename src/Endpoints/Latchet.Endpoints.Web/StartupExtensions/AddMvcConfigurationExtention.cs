using FluentValidation.AspNetCore;
using Latchet.Endpoints.Web.Filters;
using Latchet.Endpoints.Web.Middlewares;
using Latchet.Utilities.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Endpoints.Web.StartupExtensions
{
    public static class AddMvcConfigurationExtention
    {
        public static IServiceCollection AddLatchetMvcServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var latchetConfigurations = new LatchetConfiguration();
            configuration.GetSection(nameof(LatchetConfiguration)).Bind(latchetConfigurations);
            services.AddSingleton(latchetConfigurations);
            services.AddScoped<ValidateModelStateAttribute>();
            services.AddControllersWithViews(options =>
            {
                //options.Filters.AddService<ValidateModelStateAttribute>();
                options.Filters.Add(typeof(TrackActionPerformanceFilter));
            }).AddRazorRuntimeCompilation()
            .AddFluentValidation();

            services.AddLatchetDependencies(latchetConfigurations.AssmblyNameForLoad.Split(','));

            return services;
        }

        public static void UseLatchetMvcConfigure(this IApplicationBuilder app, Action<IEndpointRouteBuilder> configur, LatchetConfiguration configuration, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(configur);
        }
    }
}
