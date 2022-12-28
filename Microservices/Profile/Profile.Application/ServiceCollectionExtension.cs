using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Profile.Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
