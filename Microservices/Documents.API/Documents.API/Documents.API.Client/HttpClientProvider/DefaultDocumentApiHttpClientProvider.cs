using Documents.API.Client.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Client.HttpClientProvider.Abstraction;

namespace Documents.API.Client.HttpClientProvider
{
    public class DefaultDocumentApiHttpClientProvider:IDocumentApiHttpClientProvider
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
}
