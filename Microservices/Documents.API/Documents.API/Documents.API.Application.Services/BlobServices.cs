using Azure.Storage.Blobs;
using Documents.API.Application.Contracts.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Documents.API.Application.Services
{
    public interface IBlobServices
    {
        Task<BlobDTO> GetBlobByPathAsync(string containerName, string path, CancellationToken cancellationToken);
        Task UploadAsync(string containerName, string path, Stream content, CancellationToken cancellationToken);
        Task DeleteAsync(string containerName, string path, CancellationToken cancellationToken);

    }
    public class BlobServices : IBlobServices
    {
        private readonly BlobServiceClient _blobService;
        public BlobServices(BlobServiceClient blobService)
        {
            _blobService = blobService;
        }
        public async Task DeleteAsync(string containerName, string path, CancellationToken cancellationToken = default)
        {
            var blobClient = await GetBlobContainerClient(containerName, path);
            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);


        }

        public async Task<BlobDTO> GetBlobByPathAsync(string containerName, string path, CancellationToken cancellationToken = default)
        {
            var blobClient = await GetBlobContainerClient(containerName, path);
            var blobs = await blobClient.DownloadAsync(cancellationToken);
            return new BlobDTO
            {
                TypeOfContent = blobs.Value.ContentType,
                AbsoluteUri = blobClient.Uri.AbsoluteUri,
            };
        }

        public async Task UploadAsync(string containerName, string path, Stream content, CancellationToken cancellationToken = default)
        {
            var blobClient = await GetBlobContainerClient(containerName, path);
            await blobClient.UploadAsync(content, cancellationToken);

        }

        private async Task<BlobClient> GetBlobContainerClient(string containerName, string path)
        {
            var client = _blobService.GetBlobContainerClient(containerName);

            await client.CreateIfNotExistsAsync();
            return client.GetBlobClient(path);

        }
    }
}
