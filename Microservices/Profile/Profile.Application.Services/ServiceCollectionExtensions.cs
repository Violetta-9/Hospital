

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Profile.Application.Contracts.Internal;
using System.Reflection;
using Profile.Application.Helpers;

namespace Profile.Application.Services
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services, string basedAddress )
        {
            services.AddHttpClient<IAuthorizationService,AuthorizationService>(c=>
                c.BaseAddress = new Uri(basedAddress));
            services.AddSingleton<IEmailServices, EmailServices>();
        }
    }
}
