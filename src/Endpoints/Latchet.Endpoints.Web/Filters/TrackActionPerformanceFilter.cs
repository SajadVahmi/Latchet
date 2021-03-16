using Latchet.Utilities.Services.Logger;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Endpoints.Web.Filters
{
    public class TrackActionPerformanceFilter : IActionFilter
    {
        private Stopwatch timer;
        private readonly ILogger<TrackActionPerformanceFilter> logger;
        private readonly IScopeInformation scopeInfo;
        private IDisposable userScope;
        private IDisposable hostScope;
        private IDisposable requestScope;

        public TrackActionPerformanceFilter(
            ILogger<TrackActionPerformanceFilter> logger,
            IScopeInformation scopeInfo)
        {
            this.logger = logger;
            this.scopeInfo = scopeInfo;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            timer = new Stopwatch();

            var userDict = new Dictionary<string, string>
            {
                { "UserId", context.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value },
                { "OAuth2 Scopes", string.Join(",",
                        context.HttpContext.User.Claims.Where(c => c.Type == "scope")?.Select(c => c.Value)) }
            };
            userScope = logger.BeginScope(userDict);
            hostScope = logger.BeginScope(scopeInfo.HostScopeInfo);
            requestScope = logger.BeginScope(scopeInfo.RequestScopeInfo);

            timer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            timer.Stop();
            if (context.Exception == null)
            {
                string message = $"{context.HttpContext.Request.Path} {context.HttpContext.Request.Method} " +
                    $"code took {timer.ElapsedMilliseconds}.";
                logger.Log(LogLevel.Information, 0, message);
            }
            else
            {

            }
            userScope?.Dispose();
            hostScope?.Dispose();
            requestScope.Dispose();
        }
    }
}
