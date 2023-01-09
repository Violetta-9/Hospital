using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorStatus;

public class UpdateDoctorStatusCommandHandler : IRequestHandler<UpdateDoctorStatusCommand, Response>
{
    private readonly IDoctorRepository _doctorRepository;

    public UpdateDoctorStatusCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Response> Handle(UpdateDoctorStatusCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetDoctorByAccountIdAsync(request.AccountId, cancellationToken);
        if (doctor == null) return Response.Error;
        doctor.StatusId = request.NewStatus;
        await _doctorRepository.UpdateAsync(doctor, cancellationToken);
        return Response.Success;
    }
}