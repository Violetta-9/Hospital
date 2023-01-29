using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using Documents.API.Application.Configurations;
using Documents.API.Application.Contracts.Outgoing;
using Documents.API.Application.Services;
using MediatR;

namespace Documents.API.Application.Query.GetBlob
{
    internal class GetBlobQueryHandler : IRequestHandler<GetBlobQuery, BlobDTO>
    {

        private readonly IDocumentsRepository _documentsRepository;
        private readonly IBlobServices _blobServices;
        

        public GetBlobQueryHandler(IDocumentsRepository documentsRepository, IBlobServices blobServices)
        {
            _documentsRepository = documentsRepository;
            _blobServices = blobServices;
           
        }
        public async Task<BlobDTO> Handle(GetBlobQuery request, CancellationToken cancellationToken)
        {
            var document = await _documentsRepository.GetAsync(request.DocumentId, cancellationToken);
            return await _blobServices.GetBlobByPathAsync(document.ContainerName, document.Path, cancellationToken);
        }
    }
}
