using Latchet.Domain.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Endpoints.Web.StartupExtensions
{
    public static class AddDataAccessExtentsion
    {

        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddRepositories(assembliesForSearch).AddUnitOfWorks(assembliesForSearch);

        public static IServiceCollection AddRepositories(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandRepository<>), typeof(IQueryRepository));

        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(IUnitOfWork));
    }
}
