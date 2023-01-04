

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Profile.Application.Services
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationService,AuthorizationService>();
            services.AddSingleton<IEmailServices, EmailServices>();
        }
    }
}
