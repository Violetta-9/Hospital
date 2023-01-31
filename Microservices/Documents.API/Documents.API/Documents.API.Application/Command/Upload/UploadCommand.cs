using Documents.API.Application.Contracts.Enum;
using Documents.API.Application.Contracts.Incoming;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Application.Command.Upload;

public class UploadCommand : IRequest<long>
{
    public IFormFile File { get; set; }
    public long EntityId { get; set; }
    public SubjectUpdate Subject { get; set; }

    public UploadCommand(UploadFileDTO uploadFileDto)
    {
        File = uploadFileDto.File;
        EntityId = uploadFileDto.EntityId;
        Subject = uploadFileDto.Subject;
    }
}