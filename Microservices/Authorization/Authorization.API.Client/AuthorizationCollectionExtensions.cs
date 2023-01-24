using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.API.Client.Abstraction;
using Authorization.API.Client.Configuration;
using Authorization.API.Client.HttpClientProvider.Abstraction;
using Authorization.API.Client.HttpClientProvider;

namespace Authorization.API.Client
{
    public static class AuthorizationCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationApi(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<AuthorizationApiOptions>(configuration.GetSection("AuthorizationApi"));
            service.AddScoped<IAuthorizationApiProxy, AuthorizationApiProxy>();
            service.AddScoped<IAuthorizationApiHttpClientProvider, DefaultAuthorizationApiHttpClientProvider>();
           

            return service;
        }

    }
}
