using MediatR;
using Office.Application.Contracts.Incoming;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Command.UpdateOfficeStatus;

public class UpdateOfficeStatusCommand : IRequest<Response>
{
    public long OfficeId { get; set; }
    public bool IsActive { get; set; }

    public UpdateOfficeStatusCommand(UpdateOfficeStatusDTO updateStatuses)
    {
        OfficeId = updateStatuses.OfficeId;
        IsActive = updateStatuses.IsActive;
    }
}