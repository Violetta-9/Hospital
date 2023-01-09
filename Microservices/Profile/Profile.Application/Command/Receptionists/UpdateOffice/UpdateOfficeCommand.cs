using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.UpdateOffice;

public class UpdateOfficeCommand : IRequest<Response>
{
    public string AccountId { get; set; }
    public long NewOffice { get; set; }

    public UpdateOfficeCommand(UpdateReceptionistDTO receptionistDto)
    {
        AccountId = receptionistDto.AccountId;
        NewOffice = receptionistDto.OfficeId;
    }
}