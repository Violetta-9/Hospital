using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Doctors.DeleteDoctor
{
    public class DeleteDoctorCommand : IRequest<Response>
    {
        public string AccountId { get; set; }
        public DeleteDoctorCommand(string accountId)
        {
            AccountId = accountId;
        }
    }
}
