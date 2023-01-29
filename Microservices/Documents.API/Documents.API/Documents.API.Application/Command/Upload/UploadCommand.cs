using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.Contracts.Enum;
using Documents.API.Application.Contracts.Incoming;
using MediatR;
using Documents.API.Application.Contracts.Outgoing;

namespace Documents.API.Application.Command.Upload
{
    public class UploadCommand:IRequest<long>
    {
        public IFormFile File { get; set; }
        public long EntityId { get; set; }
        public SubjectUpdate Subject { get; set; }

        public UploadCommand(UploadFileDTO uploadFileDto,IFormFile file)
        {
            File = file;
            EntityId=uploadFileDto.EntityId;
            Subject = uploadFileDto.Subject;

        }
    }
}
