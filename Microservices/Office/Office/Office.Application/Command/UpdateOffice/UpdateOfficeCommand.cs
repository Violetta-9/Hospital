using MediatR;
using Office.Application.Contracts.Incoming;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Command.UpdateOffice;

public class UpdateOfficeCommand : IRequest<Response>
{
    public UpdateOfficeDTO UpdateOfficeDto { get; set; }

    public UpdateOfficeCommand(UpdateOfficeDTO updateOfficeDto)
    {
        UpdateOfficeDto = updateOfficeDto;
    }
}