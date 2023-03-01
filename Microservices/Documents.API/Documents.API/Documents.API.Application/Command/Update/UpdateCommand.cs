using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Application.Command.Update
{
    public class UpdateCommand:IRequest<Response>
    {
        public long DocumentId { get; set; }
        public IFormFile File { get; set; }

        public UpdateCommand(UpdatePhotoDTO photo)
        {
            DocumentId = photo.DocumentId;
            File = photo.File;
        }
    }
}
