using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.AddReceptionistRole;

public class AddReceptionistRoleCommand : IRequest<Response>
{
    public ReceptionistDTO ReceptionistDTO { get; set; }

    public AddReceptionistRoleCommand(ReceptionistDTO receptionistDto)
    {
        ReceptionistDTO = receptionistDto;
    }
}