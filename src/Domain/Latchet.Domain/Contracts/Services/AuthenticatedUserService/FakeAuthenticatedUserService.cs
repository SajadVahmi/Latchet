using Latchet.Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Contracts.Services.AuthenticatedUserService
{
    public class FakeAuthenticatedUserService : IAuthenticatedUserService
    {
        public string GetAgent()=> "1";
        public string GetFirstname()=> "Firstname";
        public int GetId() => 1;
        public string GetIp() => "0.0.0.0";
        public string GetLastname() => "Lastname";
        public string GetUsername() => "Username";
        public bool HasAccess(string accessKey) => true;
        public bool IsCurrentUser(string userId) => true;
    }
}
