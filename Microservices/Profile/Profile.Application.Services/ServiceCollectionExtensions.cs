using Microsoft.Extensions.DependencyInjection;

namespace Profile.Application.Services;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
       
        services.AddSingleton<IEmailServices, EmailServices>();
    }
}