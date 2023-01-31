using Authorization.Data.Repository;
using Documents.API.Application.Contracts.Outgoing;
using Documents.API.Application.Services;
using MediatR;

namespace Documents.API.Application.Query.GetBlob;

internal class GetBlobQueryHandler : IRequestHandler<GetBlobQuery, BlobDTO>
{
    private readonly IBlobServices _blobServices;

    private readonly IDocumentsRepository _documentsRepository;


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