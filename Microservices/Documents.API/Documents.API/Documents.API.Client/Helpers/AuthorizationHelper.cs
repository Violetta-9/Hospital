using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.API.Client.Helpers
{
    public class AuthorizationHelper
    {
        public static bool TryGetAuthorizationTokenFromHttpHeader(IHttpContextAccessor context, out string token)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                throw new Exception("Headers have not Authorization");

            var authHeader = context.HttpContext.Request.Headers["Authorization"][0];
            if (authHeader.StartsWith("Bearer "))
            {
                token = authHeader.Substring("Bearer ".Length);
                return true;
            }

            throw new Exception("Authorization have not Bearer");
        }
    }
}
