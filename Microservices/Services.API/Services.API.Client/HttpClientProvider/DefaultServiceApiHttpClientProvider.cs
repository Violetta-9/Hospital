using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Services.API.Client.GeneratedClient;
using Services.API.Client.Helpers;
using Services.API.Client.HttpClientProvider.Abstraction;

namespace Services.API.Client.HttpClientProvider
{
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
            if (AuthorizationHelper.TryGetAuthorizationTokenFromHttpHeader(_context, out string token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return Task.FromResult(httpClient);
        }
    }
}
