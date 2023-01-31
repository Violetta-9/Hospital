using Authorization.Data.Repository;
using MassTransit;
using MediatR;
using Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorStatus;

namespace Profile.API.Consumer;

public class SpecializationStatusChangedConsumer : IConsumer<SpecializationStatusChanged.SpecializationStatusChanged>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMediator _mediator;
    private readonly IStatusRepository _statusRepository;


    public SpecializationStatusChangedConsumer(IMediator mediator, IDoctorRepository doctorRepository,
        IStatusRepository statusRepository)
    {
        _mediator = mediator;
        _doctorRepository = doctorRepository;
        _statusRepository = statusRepository;
    }


    public async Task Consume(ConsumeContext<SpecializationStatusChanged.SpecializationStatusChanged> context)
    {
        var statusId = context.Message.IsActive
            ? await _statusRepository.GetStatusIdByNameAsync("At work")
            : await _statusRepository.GetStatusIdByNameAsync("Inactive");
        var doctors = await _doctorRepository.GetDoctorBySpecializationIdAsync(context.Message.SpecializationId);
        foreach (var doc in doctors)
            await _mediator.Send(
                new UpdateDoctorStatusCommand(statusId,
                    doc.AccountId));
    }
}