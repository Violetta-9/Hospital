using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Patients.DeletePatient
{
    public class DeletePatientCommand : IRequest<Response>
    {
        public string AccountId { get; set; }
        public DeletePatientCommand(string accountId)
        {
            AccountId = accountId;
        }
    }
}
