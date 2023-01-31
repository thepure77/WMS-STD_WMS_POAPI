using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace jwt.Shared.System.Security.Principal
{
    public class IdentityResolverService : IIdentity
    {
        private readonly HttpContext Context;

        public IdentityResolverService(HttpContext context)
        {
            Context = context;
        }

        public string AuthenticationType => Context?.User?.Identity.AuthenticationType ?? null;

        public bool IsAuthenticated => Context?.User?.Identity.IsAuthenticated ?? false;

        public string Name => Context?.User?.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
    }
}
