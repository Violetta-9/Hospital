using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using Documents.API.Application.Command.Upload;
using Documents.API.Application.Configurations;
using Documents.API.Application.Services;
using Microsoft.Extensions.Options;
using Documents.API.Application.Contracts.Enum;
using MediatR;
using Documents.API.Application.Contracts.Outgoing;

namespace Documents.API.Application.Command.UploadDocuments
{
    internal class UploadDocumentsCommandHandler:IRequestHandler<UploadDocumentsCommand, long>
    {
        private readonly IBlobServices _blobService;
        private readonly BlobStorageSettings _blobStorageSettings;
        private readonly IDocumentsRepository _documentsRepository;

        public UploadDocumentsCommandHandler(IBlobServices blobService, IOptions<BlobStorageSettings> blobStorageSettings,
            IDocumentsRepository documentsRepository)
        {
            _blobService = blobService;
            _blobStorageSettings = blobStorageSettings.Value;
            _documentsRepository = documentsRepository;
        }

        public async Task<long> Handle(UploadDocumentsCommand request, CancellationToken cancellationToken)
        {
            var documentation = new Document();
            var containerName = _blobStorageSettings.ImagesContainer;
            var pathTemplate = _blobStorageSettings.ResultPathTemplate;
            var path = string.Format(pathTemplate, request.EntityId, request.File.FileName);
            try
            {
                await using (var filestream = request.File.OpenReadStream())
                {
                    await _blobService.UploadAsync(containerName, path, filestream, cancellationToken);
                }

                documentation.Path = path;
                documentation.ContainerName = containerName;
                documentation.FileName = request.File.FileName;
                documentation.ResultId = request.ResultId;
                await _documentsRepository.InsertAsync(documentation, cancellationToken);
                return documentation.Id;
            }
            catch (Exception ex)
            {
                await _blobService.DeleteAsync(containerName, path, cancellationToken);
            }

            return documentation.Id; 

        }
    }
}
