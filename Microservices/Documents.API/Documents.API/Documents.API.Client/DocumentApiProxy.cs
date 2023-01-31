using Documents.API.Client.Abstraction;
using Documents.API.Client.Configuration;
using Documents.API.Client.GeneratedClient;
using Documents.API.Client.HttpClientProvider.Abstraction;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Documents.API.Client;

public class DocumentApiProxy : IDocumentApiProxy
{
    private readonly IDocumentApiHttpClientProvider _httpClientProvider;
    private string BaseUrl { get; }

    public DocumentApiProxy(IDocumentApiHttpClientProvider httpClientProvider,
        IOptions<DocumentApiOptions> documentOptions)
    {
        BaseUrl = documentOptions.Value.DocumentUrl;
        _httpClientProvider = httpClientProvider;
    }

    public async Task<long> UploadBlobAsync(FileParameter file, long entityId, SubjectUpdate subjectUpdate,
        CancellationToken cancellationToken)
    {
        var api = await GetApiClientAsync(cancellationToken);
        try
        {
            var response = await api.UploadBlobAsync(file, entityId, subjectUpdate, cancellationToken);
            return response;
        }
        catch (ApiException e)
        {
            var error = JsonConvert.DeserializeObject<ResponseDetail.ResponseDetail>(e.Response);
            throw new ValidationException(new List<ValidationFailure>
            {
                new(string.Empty, error?.Detail)
            });
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<Response> DeleteBlobAsync(long documentId, CancellationToken cancellationToken)
    {
        var api = await GetApiClientAsync(cancellationToken);
        try
        {
            var response = await api.DeleteBlobAsync(new DeleteOrGetFileDTO
            {
                DocumentId = documentId
            }, cancellationToken);
            return response;
        }
        catch (ApiException e)
        {
            var error = JsonConvert.DeserializeObject<ResponseDetail.ResponseDetail>(e.Response);
            throw new ValidationException(new List<ValidationFailure>
            {
                new(string.Empty, error?.Detail)
            });
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<BlobDTO> GetBlobAsync(long documentId, CancellationToken cancellationToken)
    {
        var api = await GetApiClientAsync(cancellationToken);
        try
        {
            var response = await api.GetBlobAsync(documentId, cancellationToken);
            return response;
        }
        catch (ApiException e)
        {
            var error = JsonConvert.DeserializeObject<ResponseDetail.ResponseDetail>(e.Response);
            throw new ValidationException(new List<ValidationFailure>
            {
                new(string.Empty, error?.Detail)
            });
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<DocumentApi> GetApiClientAsync(CancellationToken cancellationToken = default)
    {
        return new DocumentApi(BaseUrl, await _httpClientProvider.GetHttpClientAsync(cancellationToken));
    }
}