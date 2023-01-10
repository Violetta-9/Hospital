using MediatR;
using Office.Application.Contracts.Incoming;

namespace Office.Application.Command.CreateOffice;

public class CreateOfficeCommand : IRequest<long>
{
    public CreateOfficeDTO OfficeDto { get; set; }

    public CreateOfficeCommand(CreateOfficeDTO officeDto)
    {
        OfficeDto = officeDto;
    }
}