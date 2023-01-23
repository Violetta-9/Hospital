namespace Services.API.Client.HttpClientProvider.Abstraction;

public interface IServiceApiHttpClientProvider
{
    public Task<HttpClient> GetHttpClientAsync(CancellationToken cancellationToken);
}