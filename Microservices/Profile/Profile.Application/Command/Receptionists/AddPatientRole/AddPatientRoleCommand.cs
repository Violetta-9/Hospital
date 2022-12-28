using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.AddPatientRole
{
    public class AddPatientRoleCommand:IRequest<Response>
    {
        public string UserId { get; set; }
        public AddPatientRoleCommand(string userId)
        {
            UserId = userId;
        }
    }
}
