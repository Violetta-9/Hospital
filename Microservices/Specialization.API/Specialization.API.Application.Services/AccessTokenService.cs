using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Specialization.API.Application.Contracts.Outgoing;
using Specialization.API.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Services
{
    
    public interface IAccessTokenService
    {

        public string GetAccessToken();
    }

    public class AccessTokenService : IAccessTokenService
    {
        private readonly IHttpContextAccessor _context;
        public AccessTokenService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetAccessToken()
        {
            if (!_context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                throw new Exception("Headers have not Authorization");
            }

            var authHeader = _context.HttpContext.Request.Headers["Authorization"][0];
            if (authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length);
                return token;
            }
            throw new Exception("Authorization have not Bearer");
        }
    }
}
