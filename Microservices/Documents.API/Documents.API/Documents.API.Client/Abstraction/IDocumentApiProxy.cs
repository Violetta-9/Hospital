using Documents.API.Client.GeneratedClient;

namespace Documents.API.Client.Abstraction;

public interface IDocumentApiProxy
{
    public Task<long> UploadBlobAsync(FileParameter file, long entityId, long? resultId, SubjectUpdate subjectUpdate,
        CancellationToken cancellationToken);

    public Task<Response> DeleteBlobAsync(long documentId, CancellationToken cancellationToken);
    public Task<BlobDTO> GetBlobAsync(long documentId, bool isPhoto, CancellationToken cancellationToken);
    public Task<long> UpdateBlobAsync(FileParameter file, long photoId, SubjectUpdate subjectUpdate,
        CancellationToken cancellationToken);
}