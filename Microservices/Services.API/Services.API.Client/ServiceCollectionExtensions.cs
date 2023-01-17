using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Services.API.Client.Abstraction;
using Services.API.Client.Configuration;
using Services.API.Client.HttpClientProvider.Abstraction;
using Services.API.Client.HttpClientProvider;
using Microsoft.Extensions.Configuration;

namespace Services.API.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceApi(this IServiceCollection service,IConfiguration configuration)
        {
            
            service.Configure<ServiceApiOptions>(configuration.GetSection("ServiceApi"));
            service.AddScoped<IServiceApiProxy,ServiceApiProxy>();
            service.AddScoped<IServiceApiHttpClientProvider, DefaultServiceApiHttpClientProvider>();
   
            return service;
        }

        public static void RegisterHttpAccess(this IServiceCollection services, Uri serviceBaseUri)
        {
            services.AddHttpClient("ServicesClient", config =>
            {
                config.BaseAddress = serviceBaseUri;

            });
        }
    }
}
