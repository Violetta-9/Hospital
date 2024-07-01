using Authorization.Data_Domain.Models;
using Documents.API.Application.Configurations;
using Microsoft.Extensions.Options;
using Documents.API.Application.Contracts.Enum;

namespace Documents.API.Application.Services
{
    public interface IPhotoService
    {
        public Photo CreatePhoto(SubjectUpdate subject, long entityId, string fileName);

    }
    public class PhotoService : IPhotoService
    {
        private readonly BlobStorageSettings _blobStorageSettings;

        public PhotoService(IOptions<BlobStorageSettings> blobStorageSettings)
        {
            _blobStorageSettings = blobStorageSettings.Value;
        }
        public Photo CreatePhoto(SubjectUpdate subject, long entityId, string fileName)
        {
            var documentation = new Photo();
            var containerName = _blobStorageSettings.ImagesContainer;
            var pathTemplate = GetTemplatePath(subject);
            var path = string.Format(pathTemplate, entityId, fileName);

            documentation.Path = path;
            documentation.ContainerName = containerName;
            documentation.FileName = fileName;
            return documentation;
        }
        private string GetTemplatePath(SubjectUpdate subject)
        {
            switch (subject)
            {
                case SubjectUpdate.Doctor:
                    return _blobStorageSettings.DoctorPathTemplate;

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
