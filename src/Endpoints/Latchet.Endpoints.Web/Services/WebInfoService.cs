using Latchet.Endpoints.Web.Extensions;
using Latchet.Utilities.Services.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Endpoints.Web.Services
{
    public class WebUserInfoService : IUserInfoService
    {
        private readonly HttpContext httpContext;
        private const string AccessList = "";

        public WebUserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public string GetUserAgent() => httpContext.Request.Headers["User-Agent"];
        public string GetUserIp() => httpContext.Connection.RemoteIpAddress.ToString();
        public int UserId() => int.Parse(httpContext.User?.GetClaim(ClaimTypes.NameIdentifier));
        public string GetUsername() => httpContext.User?.GetClaim(ClaimTypes.Name);
        public string GetFirstName() => httpContext.User?.GetClaim(ClaimTypes.GivenName);
        public string GetLastName() => httpContext.User?.GetClaim(ClaimTypes.Surname);
        public bool IsCurrentUser(string userId)
        {
            return string.Equals(UserId().ToString(), userId, StringComparison.OrdinalIgnoreCase);
        }

        public virtual bool HasAccess(string accessKey)
        {
            var result = false;

            if (!string.IsNullOrWhiteSpace(accessKey))
            {
                var accessList = httpContext.User?.GetClaim(AccessList)?.Split(',').ToList() ?? new List<string>();
                result = accessList.Any(key => string.Equals(key, accessKey, StringComparison.OrdinalIgnoreCase));
            }

            return result;
        }
    }
}
