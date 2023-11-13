using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using Documents.API.Application.Configurations;
using Documents.API.Application.Contracts.Enum;
using Documents.API.Application.Contracts.Outgoing;
using Documents.API.Application.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace Documents.API.Application.Command.Update
{
    public class UpdateCommandHandler:IRequestHandler<UpdateCommand,long>
    {
        private readonly IBlobServices _blobService;
        private readonly BlobStorageSettings _blobStorageSettings;
        private readonly IDocumentsRepository _documentsRepository;

        public UpdateCommandHandler(IBlobServices blobService, IOptions<BlobStorageSettings> blobStorageSettings,
            IDocumentsRepository documentsRepository)
        {
            _blobService = blobService;
            _blobStorageSettings = blobStorageSettings.Value;
            _documentsRepository = documentsRepository;
        }
        public async Task<long> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var document = await _documentsRepository.GetAsync(request.FileDto.PhotoId, cancellationToken);
            // _documentsRepository.DeleteAsync(document.Id, cancellationToken);
            await _blobService.DeleteAsync(document.ContainerName, document.Path, cancellationToken);
            string directory = System.IO.Path.GetDirectoryName(document.Path);
            string newPath = System.IO.Path.Combine(directory, request.FileDto.File.FileName);
            document.Path = newPath;
            document.FileName = request.FileDto.File.FileName;
           
            var pathTemplate = GetTemplatePath(request.FileDto.Subject);
           
            try
            {
                await using (var filestream = request.FileDto.File.OpenReadStream())
                {
                    await _blobService.UploadAsync(document.ContainerName, newPath, filestream, cancellationToken);
                }

            }
            catch (Exception ex)
            {
                await _blobService.DeleteAsync(document.ContainerName, newPath, cancellationToken);
            }

            await  _documentsRepository.UpdateAsync(document, cancellationToken);
            return document.Id;
        }
        private string GetTemplatePath(SubjectUpdate subject)
        {
            string templatePath;
            switch (subject)
            {
                case SubjectUpdate.Doctor:
                    return templatePath = _blobStorageSettings.DoctorPathTemplate;

                case SubjectUpdate.Receptionist:
                    return _blobStorageSettings.ReceptionistPathTemplate;

                case SubjectUpdate.Patient:
                    return _blobStorageSettings.PatientPathTemplate;

                case SubjectUpdate.Office:
                    return _blobStorageSettings.OfficePathTemplate;
            }

            throw new Exception("Type of subject is incorrect");
        }

    }
   
}
