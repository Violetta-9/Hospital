using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.AddReceptionistRole
{
    public class AddReceptionistRoleCommand : IRequest<Response>
    {
        public string AccountId { get; set; }
        public long OfficeId { get; set; }

        public AddReceptionistRoleCommand(ReceptionistDTO receptionistDto)
        {
            AccountId = receptionistDto.AccountId;
            OfficeId = receptionistDto.OfficeId;
        }
    }
}
