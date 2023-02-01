namespace Documents.API.Client.HttpClientProvider.Abstraction;

public interface IDocumentApiHttpClientProvider
{
    public Task<HttpClient> GetHttpClientAsync(CancellationToken cancellationToken);
}