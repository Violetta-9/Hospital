using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.API.Client.HttpClientProvider.Abstraction
{
    public interface IServiceApiHttpClientProvider
    {
        public Task<HttpClient> GetHttpClientAsync(CancellationToken cancellationToken);
    }
}
