using Authorization.Data.Repository;
using Documents.API.Application.Services;
using MediatR;

namespace Documents.API.Application.Command.Upload;

internal class UploadCommandHandler : IRequestHandler<UploadCommand, long>
{
    private readonly IBlobServices _blobService;
    private readonly IPhotosRepository _documentsRepository;
    private readonly IPhotoService _photoService;

    public UploadCommandHandler(IBlobServices blobService,
        IPhotosRepository documentsRepository, IPhotoService photoService)
    {
        _blobService = blobService;
        _documentsRepository = documentsRepository;
        _photoService = photoService;
    }

    public async Task<long> Handle(UploadCommand request, CancellationToken cancellationToken)
    {
        var photo = _photoService.CreatePhoto(request.Subject, request.EntityId, request.File.FileName);
        try
        {
            await using (var filestream = request.File.OpenReadStream())
            {
                await _blobService.UploadAsync(photo.ContainerName, photo.Path, filestream, cancellationToken);
            }

            await _documentsRepository.InsertAsync(photo, cancellationToken);
            return photo.Id;
        }
        catch (Exception ex)
        {
            await _blobService.DeleteAsync(photo.ContainerName, photo.Path, cancellationToken);
        }

        return photo.Id;
    }
}