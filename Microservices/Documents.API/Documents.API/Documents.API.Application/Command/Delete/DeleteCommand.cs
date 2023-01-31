using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using MediatR;

namespace Documents.API.Application.Command.Delete;

public class DeleteCommand : IRequest<Response>
{
    public long DocumentId { get; set; }


    public DeleteCommand(DeleteOrGetFileDTO entityDto)
    {
        DocumentId = entityDto.DocumentId;
    }
}