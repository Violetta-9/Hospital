using Microsoft.AspNetCore.Http;

namespace Services.API.Client.Helpers;

public static class AuthorizationHelper
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