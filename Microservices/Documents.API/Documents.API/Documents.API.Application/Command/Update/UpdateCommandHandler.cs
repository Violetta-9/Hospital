using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using Authorization.Data.Shared.Migrations;
using Documents.API.Application.Contracts.Outgoing;
using Documents.API.Application.Services;
using MediatR;

namespace Documents.API.Application.Command.Update
{
    internal class UpdateCommandHandler : IRequestHandler<UpdateCommand, Response>
    {
        private readonly IBlobServices _blobServices;
        private readonly IDocumentsRepository _documentsRepository;
        public UpdateCommandHandler(IBlobServices blobServices, IDocumentsRepository documentsRepository)
        {
            _blobServices = blobServices;
            _documentsRepository = documentsRepository;
        }

        public async Task<Response> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var document = await _documentsRepository.GetAsync(request.DocumentId, cancellationToken);

            string pattern = @"/[^/]+$"; // регулярное выражение для поиска последней части после последнего символа "/"
            string replacement = string.Concat("/", request.File.FileName); // заменяем на пустую строку
            await _blobServices.DeleteAsync(document.ContainerName, document.Path, cancellationToken);
            document.FileName =request.File.FileName;
            document.Path = Regex.Replace(document.Path, pattern, replacement); // выполняем замену с помощью Regex.Replace

            await using (var filestream = request.File.OpenReadStream())
            {
                await _blobServices.UploadAsync(document.ContainerName, document.Path, filestream, cancellationToken);
            }

            await _documentsRepository.UpdateAsync(document, cancellationToken);
            return Response.Success;

        }
    }
}
