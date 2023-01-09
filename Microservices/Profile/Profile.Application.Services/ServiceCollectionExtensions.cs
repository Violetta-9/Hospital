using Microsoft.Extensions.DependencyInjection;

namespace Profile.Application.Services;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, string basedAddress)
    {
        services.AddHttpClient<IAuthorizationService, AuthorizationService>(c =>
            c.BaseAddress = new Uri(basedAddress));
        services.AddSingleton<IEmailServices, EmailServices>();
    }
}