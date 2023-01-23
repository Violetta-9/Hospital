namespace Authorization.API.Client.HttpClientProvider.Abstraction;

public interface IAuthorizationApiHttpClientProvider
{
    public Task<HttpClient> GetHttpClientAsync(CancellationToken cancellationToken);
}