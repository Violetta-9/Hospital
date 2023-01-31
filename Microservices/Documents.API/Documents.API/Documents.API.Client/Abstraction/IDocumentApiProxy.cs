
using Documents.API.Client.GeneratedClient;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Client.Abstraction
{
    public interface IDocumentApiProxy
    {
        public Task<long> UploadBlobAsync(FileParameter file,long entityId,SubjectUpdate subjectUpdate,CancellationToken cancellationToken);
        public Task<Response> DeleteBlobAsync(long documentId,CancellationToken cancellationToken);
        public Task<BlobDTO> GetBlobAsync(long documentId,CancellationToken cancellationToken);
    
    }
}
