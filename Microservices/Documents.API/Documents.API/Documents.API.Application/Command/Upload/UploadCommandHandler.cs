using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using Documents.API.Application.Configurations;
using Documents.API.Application.Contracts.Enum;
using Documents.API.Application.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace Documents.API.Application.Command.Upload;

internal class UploadCommandHandler : IRequestHandler<UploadCommand, long>
{
    private readonly IBlobServices _blobService;
    private readonly BlobStorageSettings _blobStorageSettings;
    private readonly IDocumentsRepository _documentsRepository;

    public UploadCommandHandler(IBlobServices blobService, IOptions<BlobStorageSettings> blobStorageSettings,
        IDocumentsRepository documentsRepository)
    {
        _blobService = blobService;
        _blobStorageSettings = blobStorageSettings.Value;
        _documentsRepository = documentsRepository;
    }

    public async Task<long> Handle(UploadCommand request, CancellationToken cancellationToken)
    {
        var documentation = new Photo();
        var containerName = _blobStorageSettings.ImagesContainer;
        var pathTemplate = GetTemplatePath(request.Subject);
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

            await _documentsRepository.InsertAsync(documentation, cancellationToken);
            return documentation.Id;
        }
        catch (Exception ex)
        {
            await _blobService.DeleteAsync(containerName, path, cancellationToken);
        }

        return documentation.Id;
        
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