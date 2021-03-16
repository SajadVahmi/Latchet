using Latchet.Application.Commands;
using Latchet.Application.Events;
using Latchet.Application.Queries;
using Latchet.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Endpoints.Web.Extensions
{
    public static class HttpContextExtentions
    {
        public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
            (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

        public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
            (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));
        public static IEventDispatcher EventDispatcher(this HttpContext httpContext) =>
            (IEventDispatcher)httpContext.RequestServices.GetService(typeof(IEventDispatcher));
        public static LatchetServices LatchetApplicationContext(this HttpContext httpContext) =>
            (LatchetServices)httpContext.RequestServices.GetService(typeof(LatchetServices));
    }
}
