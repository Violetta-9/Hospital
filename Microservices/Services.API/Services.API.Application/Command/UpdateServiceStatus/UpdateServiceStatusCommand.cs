using MediatR;
using Services.API.Contracts.Incoming;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Command.UpdateServiceStatus;

public class UpdateServiceStatusCommand : IRequest<Response>
{
    public UpdateServiceStatusDTO UpdateServiceStatusDTO { get; set; }

    public UpdateServiceStatusCommand(UpdateServiceStatusDTO updateServiceStatusDTO)
    {
        UpdateServiceStatusDTO = updateServiceStatusDTO;
    }
}