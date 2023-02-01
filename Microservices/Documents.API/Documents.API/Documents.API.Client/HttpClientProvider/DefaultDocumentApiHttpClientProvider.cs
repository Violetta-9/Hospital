using System.Net.Http.Headers;
using Documents.API.Client.Helpers;
using Documents.API.Client.HttpClientProvider.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Client.HttpClientProvider;

public class DefaultDocumentApiHttpClientProvider : IDocumentApiHttpClientProvider
{
    private readonly IHttpContextAccessor _context;

    public DefaultDocumentApiHttpClientProvider(IHttpContextAccessor context)
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