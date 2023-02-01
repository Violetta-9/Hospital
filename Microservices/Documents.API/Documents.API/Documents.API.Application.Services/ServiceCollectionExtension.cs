using Microsoft.Extensions.DependencyInjection;

namespace Documents.API.Application.Services;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IBlobServices, BlobServices>();
    }
}