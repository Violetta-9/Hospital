using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.API.Client.HttpClientProvider.Abstraction
{
    public interface IDocumentApiHttpClientProvider
    {
        public Task<HttpClient> GetHttpClientAsync(CancellationToken cancellationToken);
    }
}
