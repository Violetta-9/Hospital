using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorStatus;

public class UpdateDoctorStatusCommand : IRequest<Response>
{
    public long NewStatus { get; set; }
    public string AccountId { get; set; }

    public UpdateDoctorStatusCommand(long status, string accounrId)
    {
        NewStatus = status;
        AccountId = accounrId;
    }
}