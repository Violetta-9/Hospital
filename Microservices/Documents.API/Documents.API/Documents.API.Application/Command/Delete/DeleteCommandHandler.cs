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

namespace Documents.API.Application.Command.Delete
{
    internal class DeleteCommandHandler : IRequestHandler<DeleteCommand, Response>
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IBlobServices _blobServices;
        

        public DeleteCommandHandler(IDocumentsRepository documentsRepository,IBlobServices blobServices)
        {
            _documentsRepository= documentsRepository;
            _blobServices= blobServices;
            
        }
        public async Task<Response> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var document = await _documentsRepository.GetAsync(request.DocumentId, cancellationToken);
            await _documentsRepository.DeleteAsync(document.Id, cancellationToken);
            await _blobServices.DeleteAsync(document.ContainerName, document.Path, cancellationToken);
            return Response.Success;
        }
    }
}
