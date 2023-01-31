using Documents.API.Client.Abstraction;
using Documents.API.Client.Configuration;
using Documents.API.Client.HttpClientProvider;
using Documents.API.Client.HttpClientProvider.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Documents.API.Client;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDocumentsApi(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<DocumentApiOptions>(configuration.GetSection("DocumentApi"));
        service.AddScoped<IDocumentApiProxy, DocumentApiProxy>();
        service.AddScoped<IDocumentApiHttpClientProvider, DefaultDocumentApiHttpClientProvider>();
        return service;
    }
}