using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Services.API.Client.Helpers;
using Services.API.Client.HttpClientProvider.Abstraction;

namespace Services.API.Client.HttpClientProvider;

public class DefaultServiceApiHttpClientProvider : IServiceApiHttpClientProvider
{
    private readonly IHttpContextAccessor _context;

    public DefaultServiceApiHttpClientProvider(IHttpContextAccessor context)
    {
        _context = context;
    }

    public Task<HttpClient> GetHttpClientAsync(CancellationToken cancellationToken)
    {
        var httpClient = new HttpClient();
        if (AuthorizationHelper.TryGetAuthorizationTokenFromHttpHeader(_context, out var token))
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return Task.FromResult(httpClient);
    }
}