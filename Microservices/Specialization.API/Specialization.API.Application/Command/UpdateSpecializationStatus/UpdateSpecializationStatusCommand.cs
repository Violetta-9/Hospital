using MediatR;
using Specialization.API.Application.Contracts.Incoming;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Command.UpdateSpecializationStatus;

public class UpdateSpecializationStatusCommand : IRequest<Response>
{
    public UpdateSpecializationStatusDTO UpdateSpecializationStatusDto { get; set; }

    public UpdateSpecializationStatusCommand(UpdateSpecializationStatusDTO updateSpecializationStatusDto)
    {
        UpdateSpecializationStatusDto = updateSpecializationStatusDto;
    }
}