using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using MediatR;

namespace Documents.API.Application.Query.GetBlob;

public class GetBlobQuery : IRequest<BlobDTO>
{
    public long DocumentId { get; set; }


    public GetBlobQuery(DeleteOrGetFileDTO entityDto)
    {
        DocumentId = entityDto.DocumentId;
    }
}