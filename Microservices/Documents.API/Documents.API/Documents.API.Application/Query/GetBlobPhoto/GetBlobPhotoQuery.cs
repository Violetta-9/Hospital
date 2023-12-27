using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using MediatR;

namespace Documents.API.Application.Query.GetBlobPhoto
{
    public class GetBlobPhotoQuery : IRequest<BlobDTO>
    {
        public long DocumentId { get; set; }


        public GetBlobPhotoQuery(DeleteOrGetFileDTO entityDto)
        {
            DocumentId = entityDto.DocumentId;
        }
    }
}
