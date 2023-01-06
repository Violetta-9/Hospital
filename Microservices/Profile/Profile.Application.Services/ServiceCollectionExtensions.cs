

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Profile.Application.Services
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthorizationService,AuthorizationService>(c=>
                c.BaseAddress = new Uri("https://localhost:44336/"));
            services.AddSingleton<IEmailServices, EmailServices>();
        }
    }
}
