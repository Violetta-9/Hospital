using Authorization.Data.Repository;
using Documents.API.Application.Contracts.Outgoing;
using Documents.API.Application.Services;
using MediatR;

namespace Documents.API.Application.Command.Delete;

internal class DeleteCommandHandler : IRequestHandler<DeleteCommand, Response>
{
    private readonly IBlobServices _blobServices;
    private readonly IPhotosRepository _documentsRepository;


    public DeleteCommandHandler(IPhotosRepository documentsRepository, IBlobServices blobServices)
    {
        _documentsRepository = documentsRepository;
        _blobServices = blobServices;
    }

    public async Task<Response> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var document = await _documentsRepository.GetAsync(request.DocumentId, cancellationToken);
        await _documentsRepository.DeleteAsync(document.Id, cancellationToken);
        await _blobServices.DeleteAsync(document.ContainerName, document.Path, cancellationToken);
        return Response.Success;
    }
}