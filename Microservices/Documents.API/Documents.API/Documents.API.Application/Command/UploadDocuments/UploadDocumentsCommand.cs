using Documents.API.Application.Contracts.Enum;
using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Application.Command.UploadDocuments
{
    public class UploadDocumentsCommand:IRequest<long>
    {
        public IFormFile File { get; set; }
        public long EntityId { get; set; }
        public long ResultId { get; set; }
        public SubjectUpdate Subject { get; set; }

        public UploadDocumentsCommand(UploadFileDTO uploadFileDto)
        {
            File = uploadFileDto.File;
            EntityId = uploadFileDto.EntityId;
            Subject = uploadFileDto.Subject;
            ResultId =(long) uploadFileDto.ResultId;
        }
    }
}
