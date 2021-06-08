using Latchet.Domain.Services.AuthenticatedUserService;
using Latchet.Domain.Services.JsonSerializer;
using Latchet.Domain.Services.ObjectMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Domain.Services.Common
{
    public class LatchetServices
    {
       
        public readonly IObjectMapper ObjectMapper;
        public readonly IJsonSerializer JsonSerializer;
        public readonly IAuthenticatedUserService AuthenticatedUserService;

        public LatchetServices(IObjectMapper objectMapper, 
            IJsonSerializer jsonSerializer, IAuthenticatedUserService authenticatedUserService)
        {

            ObjectMapper = objectMapper;
            JsonSerializer = jsonSerializer;
            AuthenticatedUserService = authenticatedUserService;
        }
    }
}
