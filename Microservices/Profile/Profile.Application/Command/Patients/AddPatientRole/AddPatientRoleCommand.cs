using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Patients.AddPatientRole
{
    public class AddPatientRoleCommand:IRequest<Response>
    {
        public string AccountId { get; set; }
        public AddPatientRoleCommand(string userId)
        {
            AccountId = userId;
        }
    }
}
