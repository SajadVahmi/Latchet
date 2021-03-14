using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Services.Users
{
    public interface IUserInfoService
    {
        string GetUserAgent();
        string GetUserIp();
        int UserId();

        string GetFirstName();
        string GetLastName();
        string GetUsername();
        bool IsCurrentUser(string userId);
        bool HasAccess(string accessKey);
    }
}
