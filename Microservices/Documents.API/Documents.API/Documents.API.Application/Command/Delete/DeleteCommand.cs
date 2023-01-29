using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.Contracts.Enum;
using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using MediatR;

namespace Documents.API.Application.Command.Delete
{
    public class DeleteCommand:IRequest<Response>
    {
        public long DocumentId { get; set; }
       

        public DeleteCommand(DeleteOrGetFileDTO entityDto)
        {
            DocumentId=entityDto.DocumentId;
           
        }
    }
}
