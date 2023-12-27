using Authorization.Data.Repository;
using Documents.API.Application.Contracts.Outgoing;
using Documents.API.Application.Services;
using MediatR;

namespace Documents.API.Application.Query.GetBlobPhoto;

internal class GetBlobPhotoQueryHandler : IRequestHandler<GetBlobPhotoQuery, BlobDTO>
{
    private readonly IBlobServices _blobServices;
    private readonly IPhotosRepository _photosRepository;

    public GetBlobPhotoQueryHandler(IBlobServices blobServices, IPhotosRepository photosRepository)
    {
       
        _blobServices = blobServices;
        _photosRepository = photosRepository;
    }

    public async Task<BlobDTO> Handle(GetBlobPhotoQuery request, CancellationToken cancellationToken)
    {
        var document = await _photosRepository.GetAsync(request.DocumentId, cancellationToken);
        return await _blobServices.GetBlobByPathAsync(document.ContainerName, document.Path, cancellationToken);
    }
}