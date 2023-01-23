using System.Net.Http.Headers;
using Authorization.API.Client.HttpClientProvider.Abstraction;
using Microsoft.AspNetCore.Http;


namespace Authorization.API.Client.HttpClientProvider;

public class DefaultAuthorizationApiHttpClientProvider : IAuthorizationApiHttpClientProvider
{
    private readonly IHttpContextAccessor _context;

    public DefaultAuthorizationApiHttpClientProvider(IHttpContextAccessor context)
    {
        _context = context;
    }

    public Task<HttpClient> GetHttpClientAsync(CancellationToken cancellationToken)
    {
        var httpClient = new HttpClient();
        return Task.FromResult(httpClient);
    }
}