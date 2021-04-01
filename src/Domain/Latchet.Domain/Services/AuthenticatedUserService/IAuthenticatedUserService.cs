using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Services.AuthenticatedUserService
{
    public interface IAuthenticatedUserService
    {
         
        string GetAgent();
        string GetIp();
        int GetId();

        string GetFirstname();
        string GetLastname();
        string GetUsername();
        bool IsCurrentUser(string userId);
        bool HasAccess(string accessKey);
    }
}

