using Documents.API.Application.Contracts.Enum;
using Documents.API.Application.Contracts.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.Contracts.Outgoing;
using MediatR;

namespace Documents.API.Application.Query.GetBlob
{
    public class GetBlobQuery:IRequest<BlobDTO>
    {
        public long DocumentId { get; set; }
      

        public GetBlobQuery(DeleteOrGetFileDTO entityDto)
        {
            DocumentId = entityDto.DocumentId;
       
        }
    }
}
