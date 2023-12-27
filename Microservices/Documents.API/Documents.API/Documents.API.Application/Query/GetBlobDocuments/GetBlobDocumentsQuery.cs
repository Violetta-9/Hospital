using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using MediatR;

namespace Documents.API.Application.Query.GetBlobDocuments;

public class GetBlobDocumentsQuery : IRequest<BlobDTO>
{
    public long DocumentId { get; set; }


    public GetBlobDocumentsQuery(DeleteOrGetFileDTO entityDto)
    {
        DocumentId = entityDto.DocumentId;
    }
}