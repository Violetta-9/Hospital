using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, string basedAddressDoctor,string basedAddressService)
        {
            //services.AddHttpClient("ServicesClient", config =>
            //{
            //    config.BaseAddress = new Uri(basedAddressService);

            //});
            //services.AddScoped<IServicesService, ServicesService>();
            //services.AddScoped<IAccessTokenService, AccessTokenService>();
        }
    }
}
